using OrdersAPI.Enums;

namespace OrdersAPI.Entities;

/// <summary>
/// Заказ.
/// </summary>
public class Order
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Статус.
    /// </summary>
    public OrderStatus Status { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Связанные сущности.
    /// </summary>
    public List<OrderLine> Lines { get; set; }
}