using OrdersAPI.Entities;
using OrdersAPI.Models;

namespace OrdersAPI.Interfaces;

/// <summary>
/// Сервис создания заказов.
/// </summary>
public interface IOrderCreateService
{
    /// <summary>
    /// Создать заказ.
    /// </summary>
    public Order Execute(OrderCreateModel model);
}