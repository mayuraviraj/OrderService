using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Contracts;
using OrderService.API.Features.CreateOrder;
using OrderService.API.Features.GetOrder;

namespace OrderService.API.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(request.CustomerName);
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
       var order = await  _mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
       return order is null ? NotFound() : Ok(order);
    }
}