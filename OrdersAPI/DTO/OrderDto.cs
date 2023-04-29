using OrdersAPI.Enums;

namespace OrdersAPI.DTO;

/// <summary>
/// DTO сущности заказа.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Статус.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Связанные сущности.
    /// </summary>
    public List<LineDto> Lines { get; set; }
}