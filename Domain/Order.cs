namespace OrderService.API.Domain;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CustomerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}