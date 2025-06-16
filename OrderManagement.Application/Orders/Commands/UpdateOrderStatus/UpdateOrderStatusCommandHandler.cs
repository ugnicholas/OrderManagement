using MediatR;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities.Order;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Application.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        // Defines the valid status transitions for an order.
        // Each key represents a current OrderStatus,
        // and its corresponding value is a list of statuses the order can transition to next.
        // This ensures that status changes follow a valid workflow and prevents invalid transitions.
        private static readonly Dictionary<OrderStatus, List<OrderStatus>> ValidTransitions = new()
        {
            { OrderStatus.Pending, new() { OrderStatus.Unpaid, OrderStatus.Cancelled } },
            { OrderStatus.Unpaid, new() { OrderStatus.Processing, OrderStatus.Cancelled } },
            { OrderStatus.Processing, new() { OrderStatus.ReadyForShipment, OrderStatus.Cancelled } },
            { OrderStatus.ReadyForShipment, new() { OrderStatus.OnboardWithCourier } },
            { OrderStatus.OnboardWithCourier, new() { OrderStatus.Complete } },
        };

        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithStatusHistoryAsync(request.OrderId) ?? throw new Exception("Order not found.");

            // Get the most recent status from the order's status history,
            // ordered by creation time (latest first). If no history exists,
            // default to 'Pending' as the initial status.
            var currentStatus = order.StatusHistory
                .OrderByDescending(s => s.CreatedAt)
                .FirstOrDefault()?.Name ?? OrderStatus.Pending;

            // Check if the current order status has any defined valid transitions,
            // and whether the requested new status is one of them.
            // If not, throw an exception to prevent an invalid status change.
            if (!ValidTransitions.TryGetValue(currentStatus, out var allowed) || !allowed.Contains(request.NewStatus))
                throw new InvalidOperationException($"Cannot transition from {currentStatus} to {request.NewStatus}.");

            // Update current status
            order.StatusId = (OrderStatus)request.NewStatus;

            // Add to status history
            order.StatusHistory.Add(new OrderStatusHistory
            {
                OrderId = request.OrderId,
                Name = request.NewStatus
            });

            _orderRepository.Update(order);
            return true;
        }
    }

}
