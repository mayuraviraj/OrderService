using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.API.Contracts;
using OrderService.API.Infrastructure;

namespace OrderService.API.Features.GetOrder;

public class GetOrdersHandler(AppDbContext db) : IRequestHandler<GetOrderQuery, PaginatedOrderDto>
{
    public async Task<PaginatedOrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var query = db.Orders.AsNoTracking(); // Tells EF Core not to track the entities — improves performance for read-only queries.
        query = request.SortBy?.ToLower() switch
        {
            "customername" when request.SortOrder?.ToLower() == "desc" => query.OrderByDescending(o => o.CustomerName),
            "customername" => query.OrderBy(o => o.CustomerName),
            _ => query.OrderBy(o => o.Id),
        };
        var totalCount = await query.CountAsync(cancellationToken);
        var orders = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(o => new OrderDto(o.Id, o.CustomerName))
            .ToListAsync(cancellationToken);

        return new PaginatedOrderDto(totalCount, request.PageNumber, request.PageSize, orders);
        
    }
}