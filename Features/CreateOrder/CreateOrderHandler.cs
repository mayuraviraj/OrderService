using MediatR;
using OrderService.API.Domain;
using OrderService.API.Infrastructure;

namespace OrderService.API.Features.CreateOrder;

public class CreateOrderHandler(AppDbContext db) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerName = request.CustomerName
        };
        db.Orders.Add(order);
        await db.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}