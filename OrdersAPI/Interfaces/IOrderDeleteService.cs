using OrdersAPI.Models;

namespace OrdersAPI.Interfaces;

/// <summary>
/// Сервис удаления заказов.
/// </summary>
public interface IOrderDeleteService
{
    /// <summary>
    /// Удалить заказ.
    /// </summary>
    public void Execute(OrderDeleteModel model);
}