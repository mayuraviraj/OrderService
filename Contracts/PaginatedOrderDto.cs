namespace OrderService.API.Contracts;

public record PaginatedOrderDto(
    int TotalCount,
    int PageNumber,
    int PageSize,
    List<OrderDto> Orders
    );