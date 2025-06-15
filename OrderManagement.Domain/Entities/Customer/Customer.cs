using OrderManagement.Domain.Common;
using OrderManagement.Domain.Enum;
using OrderManagement.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities.Customer
{
    public class Customer: BaseEntity
    {
        public string Name { get; set; }
        public CustomerSegment Segment { get; set; }
        public ICollection<Order.Order> Orders { get; set; }
    }
}
