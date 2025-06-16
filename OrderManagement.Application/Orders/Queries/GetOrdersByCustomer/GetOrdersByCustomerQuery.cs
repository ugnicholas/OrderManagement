using MediatR;
using OrderManagement.Application.Orders.Dtos;
using OrderManagement.Domain.Entities.Order;

namespace OrderManagement.Application.Orders.Queries.GetOrdersByCustomer
{
    // GetOrdersByCustomerQuery.cs
    public record GetOrdersByCustomerQuery(Guid CustomerId) : IRequest<List<OrderDto>>;

}
