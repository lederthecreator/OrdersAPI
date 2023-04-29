namespace OrdersAPI.DTO;

/// <summary>
/// DTO для редактирования записи.
/// </summary>
public class UpdateOrderDto
{
    /// <summary>
    /// Статус.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Связанные сущности.
    /// </summary>
    public List<LineDto> Lines { get; set; }
}