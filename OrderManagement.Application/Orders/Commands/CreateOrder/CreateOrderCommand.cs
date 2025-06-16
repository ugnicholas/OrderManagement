using MediatR;
using OrderManagement.Application.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public decimal Discount { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
