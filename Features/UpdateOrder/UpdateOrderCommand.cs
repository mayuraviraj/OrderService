using MediatR;

namespace OrderService.API.Features.UpdateOrder;

public record UpdateOrderCommand(Guid Id, string CustomerName) : IRequest<bool>;
