namespace OrdersAPI.Entities;

/// <summary>
/// Связанная сущность к заказу.
/// </summary>
public class OrderLine
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Количество.
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid OrderId { get; set; }
}