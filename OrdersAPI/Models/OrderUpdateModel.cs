namespace OrdersAPI.Models;

/// <summary>
/// Модель редактирования заказа.
/// </summary>
public class OrderUpdateModel
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public Guid EntityId { get; set; }
    
    /// <summary>
    /// Статус заказа.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Связанные сущности.
    /// </summary>
    public List<LineModel> Lines { get; set; }
}