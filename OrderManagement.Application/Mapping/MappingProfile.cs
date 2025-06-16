using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Mapping
{
    using AutoMapper;
    using OrderManagement.Domain.Entities.Order;
    using OrderManagement.Application.Orders.Dtos;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderStatusHistory, OrderStatusHistoryDto>();
        }
    }
}
