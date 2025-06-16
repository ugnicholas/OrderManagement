using MediatR;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities.Order;
using OrderManagement.Domain.Entities.Customer;
using System.Threading;
using System.Threading.Tasks;
using OrderManagement.Application.Orders.Commands.CreateOrder;
using OrderManagement.Domain.Enum;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IDiscountService _discountService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IDiscountService discountService,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _discountService = discountService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // 1. Load the customer
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId) ?? throw new Exception("Customer not found");

        // 2. Create the Order and calculate total
        var order = new Order
        {
            CustomerId = request.CustomerId,
            StatusId = OrderStatus.Pending // Initial status ID
        };

        foreach (var item in request.Items)
        {
            order.Items.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            });
        }

        order.Total = order.Items.Sum(i => i.Quantity * i.Price);

        // 3. Calculate Discount
        var discount = _discountService.CalculateDiscount(customer, order);
        order.Discount = discount;
        order.Total -= discount;


        // Add initial order status to history
        order.StatusHistory.Add(new OrderStatusHistory
        {
            Name = OrderStatus.Pending // Status enum or code (e.g. Created or Pending)
        });

        // 4. Save Order
        await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
