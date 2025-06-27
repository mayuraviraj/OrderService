using MediatR;
using OrderService.API.Contracts;

namespace OrderService.API.Features.GetOrder;

public class GetOrderQuery : IRequest<PaginatedOrderDto>
{
    /**
     * The PageNumber can be set only during object initialization (e.g., in an object initializer or constructor).
        After that, it's effectively immutable — like a get-only property.
    */
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SortBy { get; init; } = "CustomerName";
    public string? SortOrder { get; init; } = "asc";
}