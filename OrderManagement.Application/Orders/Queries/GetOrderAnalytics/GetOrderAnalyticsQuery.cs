using MediatR;
using OrderManagement.Application.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders.Queries.GetOrderAnalytics
{
    public class GetOrderAnalyticsQuery : IRequest<OrderAnalyticsDto>;
}
