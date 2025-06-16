using OrderManagement.Domain.Common;
using OrderManagement.Domain.Enum;
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
        private ICollection<OrderStatusHistory> _statusHistory;

        public Guid CustomerId { get; set; }

        public OrderStatus StatusId { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }

        public virtual ICollection<OrderStatusHistory> StatusHistory
        {
            get => _statusHistory ??= new List<OrderStatusHistory>();
            protected set => _statusHistory = value;
        }

        public virtual ICollection<OrderItem> Items
        {
            get => _orderItems ??= new List<OrderItem>();
            protected set => _orderItems = value;
        }
    }
}
