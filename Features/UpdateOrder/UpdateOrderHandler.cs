using MediatR;
using OrderService.API.Infrastructure;

namespace OrderService.API.Features.UpdateOrder;

public class UpdateOrderHandler(AppDbContext db) : IRequestHandler<UpdateOrderCommand, bool>
{
    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await db.Orders.FindAsync(new object[] { request.Id }, cancellationToken);
        if (order is null)
        {
            return false;
        }
        order.CustomerName = request.CustomerName;
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}