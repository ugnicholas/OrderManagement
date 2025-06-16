using MediatR;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Orders.Dtos;
using OrderManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders.Queries.GetOrderAnalytics
{
    public class GetOrderAnalyticsQueryHandler : IRequestHandler<GetOrderAnalyticsQuery, OrderAnalyticsDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderAnalyticsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderAnalyticsDto> Handle(GetOrderAnalyticsQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllWithStatusHistoryAsync(cancellationToken);

            if (orders.Count == 0)
                return new OrderAnalyticsDto();

            var totalOrders = orders.Count;
            var avgOrderValue = orders.Average(o => o.Total);

            var fulfillmentDurations = new List<TimeSpan>();
            var processingDurations = new List<TimeSpan>();
            var statusCount = new Dictionary<string, int>();

            foreach (var order in orders)
            {
                var created = order.CreatedAt;
                var complete = order.StatusHistory.FirstOrDefault(s => s.Name == OrderStatus.Complete)?.CreatedAt;

                if (complete != null)
                    fulfillmentDurations.Add(complete.Value - created);

                var processing = order.StatusHistory.FirstOrDefault(s => s.Name == OrderStatus.Processing)?.CreatedAt;
                if (processing != null && complete != null)
                    processingDurations.Add(complete.Value - processing.Value);

                var latest = order.StatusHistory
                    .OrderByDescending(s => s.CreatedAt)
                    .FirstOrDefault()?.Name.ToString() ?? "Unknown";

                if (!statusCount.ContainsKey(latest))
                    statusCount[latest] = 0;
                statusCount[latest]++;
            }

            return new OrderAnalyticsDto
            {
                TotalOrders = totalOrders,
                AverageOrderValue = Math.Round(avgOrderValue, 2),
                AverageFulfillmentTimeInHours = fulfillmentDurations.Any() ?
                    Math.Round(fulfillmentDurations.Average(d => d.TotalHours), 2) : 0,
                AverageProcessingTimeInHours = processingDurations.Any() ?
                    Math.Round(processingDurations.Average(d => d.TotalHours), 2) : 0,
                OrdersByStatus = statusCount
            };
        }
    }

}
