using OrdersAPI.Models;

namespace OrdersAPI.Interfaces;

/// <summary>
/// Сервис редактирования заказов.
/// </summary>
public interface IOrderUpdateService
{
    /// <summary>
    /// Отредактировать заказ.
    /// </summary>
    public void Execute(OrderUpdateModel model);
}