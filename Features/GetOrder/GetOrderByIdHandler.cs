using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.API.Contracts;
using OrderService.API.Infrastructure;

namespace OrderService.API.Features.GetOrder;

public class GetOrderByIdHandler: IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly AppDbContext _dbContext;

    public GetOrderByIdHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o=> o.Id == request.Id, cancellationToken);
        return order is null ? null : new OrderDto(order.Id, order.CustomerName);
    }
}