using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders.Dtos
{
    public class OrderAnalyticsDto
    {
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
        public double AverageFulfillmentTimeInHours { get; set; }
        public double AverageProcessingTimeInHours { get; set; }
        public Dictionary<string, int> OrdersByStatus { get; set; } = new();
    }
}
