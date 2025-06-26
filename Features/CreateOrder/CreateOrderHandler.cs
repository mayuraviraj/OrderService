using MediatR;
using OrderService.API.Domain;
using OrderService.API.Infrastructure;

namespace OrderService.API.Features.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly AppDbContext _db;

    public CreateOrderHandler(AppDbContext db)
    {
        _db = db;
    }
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerName = request.CustomerName
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}