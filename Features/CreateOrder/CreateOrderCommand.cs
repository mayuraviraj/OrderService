using MediatR;

namespace OrderService.API.Features.CreateOrder;

public record CreateOrderCommand(string CustomerName): IRequest<Guid>
{
}