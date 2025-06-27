using MediatR;
using OrderService.API.Contracts;

namespace OrderService.API.Features.GetOrder;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto>;
