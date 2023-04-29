namespace OrdersAPI.Models;

/// <summary>
/// Модель для удаления заказа.
/// </summary>
public class OrderDeleteModel
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid EntityId { get; set; }
}