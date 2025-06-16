using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Orders.Commands.UpdateOrderStatus;

namespace OrderManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Updates the status of an existing order.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to update.</param>
        /// <param name="request">The request payload containing the new status.</param>
        /// <returns>
        /// Returns <see cref="NoContentResult"/> if the update is successful.
        /// Returns <see cref="BadRequestObjectResult"/> if the order ID in the route doesn't match the payload.
        /// </returns>
        /// <response code="204">Status updated successfully.</response>
        /// <response code="400">The order ID in the route does not match the request body.</response>
        /// <response code="404">The order was not found.</response>
        /// <response code="409">Invalid status transition attempted.</response>
        [HttpPut("{orderId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateStatus(Guid orderId, [FromBody] UpdateOrderStatusCommand request)
        {
            if (orderId != request.OrderId)
                return BadRequest("Order ID in the route does not match the request body.");

            await _mediator.Send(request);
            return NoContent(); // 204 No Content response for successful update
        }
    }
}
