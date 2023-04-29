using Microsoft.EntityFrameworkCore;
using OrdersAPI.Entities;
using OrdersAPI.Enums;

namespace OrdersAPI.DataStore;

public class OrderingContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderLine> OrderLines { get; set; }
    
    public string DbPath { get; }
    
    public IConfiguration Configuration { get; }

    public OrderingContext(IConfiguration configuration)
    {
        Configuration = configuration;
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("OrdersDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Order>()
            .Property(x => x.Status)
            .HasConversion(
                x => x.ToString(),
                x => (OrderStatus) Enum.Parse(typeof(OrderStatus), x));
    }
}