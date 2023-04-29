namespace OrdersAPI.DTO;

/// <summary>
/// DTO создания заказа.
/// </summary>
public class CreateOrderDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Связанные сущности.
    /// </summary>
    public List<LineDto> Lines { get; set; }
}