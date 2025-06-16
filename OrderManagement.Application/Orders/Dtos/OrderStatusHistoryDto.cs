using OrderManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders.Dtos
{
    public class OrderStatusHistoryDto
    {
        public OrderStatus Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
