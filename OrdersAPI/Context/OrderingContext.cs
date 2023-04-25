using Microsoft.EntityFrameworkCore;
using OrdersAPI.Entities;

namespace OrdersAPI.Context;

public class OrderingContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderLine> OrderLines { get; set; }
    
    public string DbPath { get; }
    
    public IConfiguration Configuration { get; }

    public OrderingContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("OrdersDatabase"));
}