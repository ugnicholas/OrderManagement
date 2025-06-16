using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Orders.Commands.CreateOrder;
using OrderManagement.Application.Orders.Dtos;
using OrderManagement.Application.Orders.Queries.GetOrderAnalytics;
using OrderManagement.Application.Orders.Queries.GetOrdersByCustomer;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="command">The order creation request.</param>
        /// <returns>The created order's ID.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new order",
            Description = "Submits a new order and returns the newly created order ID"
        )]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrdersByCustomer), new { customerId = command.CustomerId }, new { orderId });
        }

        /// <summary>
        /// Retrieves orders for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>List of orders for the customer.</returns>
        [HttpGet("customer/{customerId:guid}")]
        [SwaggerOperation(
            Summary = "Get customer orders",
            Description = "Returns all orders that belong to a specific customer"
        )]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrdersByCustomer(Guid customerId)
        {
            var orders = await _mediator.Send(new GetOrdersByCustomerQuery(customerId));
            return Ok(orders);
        }

        /// <summary>
        /// Returns analytics data for orders.
        /// </summary>
        /// <returns>Analytics such as average order value and fulfillment times.</returns>
        [HttpGet("analytics")]
        [SwaggerOperation(
            Summary = "Get order analytics",
            Description = "Returns order-level statistics such as average value, fulfillment time, and more"
        )]
        [ProducesResponseType(typeof(OrderAnalyticsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAnalytics()
        {
            var analytics = await _mediator.Send(new GetOrderAnalyticsQuery());
            return Ok(analytics);
        }
    }
}
