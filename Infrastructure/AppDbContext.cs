using Microsoft.EntityFrameworkCore;
using OrderService.API.Domain;

namespace OrderService.API.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
}