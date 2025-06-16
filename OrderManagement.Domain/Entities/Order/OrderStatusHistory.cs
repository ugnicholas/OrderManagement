using OrderManagement.Domain.Common;
using OrderManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities.Order
{
    public class OrderStatusHistory : BaseEntity
    {
        public Guid OrderId { get; set; }

        public OrderStatus Name { get; set; }
    }
}
