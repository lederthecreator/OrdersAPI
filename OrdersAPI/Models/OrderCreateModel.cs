using OrdersAPI.DTO;
using OrdersAPI.Enums;

namespace OrdersAPI.Models;

/// <summary>
/// Модель создания заказа.
/// </summary>
public class OrderCreateModel
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid EntityId { get; set; }
    
    /// <summary>
    /// Связанные сущности.
    /// </summary>
    public List<LineDto> Lines { get; set; }
}