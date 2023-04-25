using OrdersAPI.Enums;

namespace OrdersAPI.Entities;

/// <summary>
/// Заказ.
/// </summary>
public class Order
{
    public Guid Id { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public DateTime Created { get; set; }
    
    public List<OrderLine> Lines { get; set; }
}