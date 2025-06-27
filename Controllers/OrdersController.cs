using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Contracts;
using OrderService.API.Features.CreateOrder;
using OrderService.API.Features.GetOrder;

namespace OrderService.API.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController(ILogger<OrdersController> logger, IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(request.CustomerName);
        var id = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching order with ID: {OrderId}", id);
        var order = await mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
        if (order == null)
        {
            logger.LogWarning("Order not found {OrderId}", id);
            return NotFound();
        }
        else
        {
            logger.LogInformation("Returning order with ID: {OrderId}", id);
            return Ok(order);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortBy = "CustomerName",
        [FromQuery] string? sortOrder = "asc",
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetOrderQuery()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SortBy = sortBy,
            SortOrder = sortOrder
        }, cancellationToken);

        return Ok(result);
    }
}