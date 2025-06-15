using OrderManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities.Order
{
    public class Order : BaseEntity
    {
        private ICollection<OrderItem> _orderItems;
        private ICollection<OrderStatus> _statusHistory;

        public Guid CustomerId { get; set; }

        public int StatusId { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }

        public virtual ICollection<OrderStatus> StatusHistory
        {
            get => _statusHistory ??= new List<OrderStatus>();
            protected set => _statusHistory = value;
        }

        public virtual ICollection<OrderItem> Items
        {
            get => _orderItems ??= new List<OrderItem>();
            protected set => _orderItems = value;
        }
    }
}
