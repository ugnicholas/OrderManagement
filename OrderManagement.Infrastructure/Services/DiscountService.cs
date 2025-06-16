using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities.Customer;
using OrderManagement.Domain.Entities.Order;
using OrderManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Services
{
    public class DiscountService: IDiscountService
    {
        public decimal CalculateDiscount(Customer customer, Order order)
        {
            var totalSpent = customer.Orders.Sum(o => o.Total);
            decimal discount = 0;

            switch (customer.Segment)
            {
                case CustomerSegment.Regular:
                    if (totalSpent > 500)
                        discount = order.Total * 0.05m;
                    break;
                case CustomerSegment.Premium:
                    discount = order.Total * 0.10m;
                    break;
                case CustomerSegment.VIP:
                    discount = order.Total * 0.15m;
                    break;
            }

            return discount;
        }
    }
}
