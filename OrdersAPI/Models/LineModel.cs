namespace OrdersAPI.Models;

/// <summary>
/// Модель строки LineOrder.
/// </summary>
public class LineModel
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