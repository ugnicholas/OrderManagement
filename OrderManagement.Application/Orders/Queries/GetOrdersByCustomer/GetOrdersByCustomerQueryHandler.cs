using AutoMapper;
using MediatR;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Orders.Dtos;
using OrderManagement.Domain.Entities.Order;

namespace OrderManagement.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerQueryHandler : IRequestHandler<GetOrdersByCustomerQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByCustomerQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(request.CustomerId);
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
