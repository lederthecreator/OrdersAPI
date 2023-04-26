using Microsoft.AspNetCore.Mvc;
using OrdersAPI.DTO;
using OrdersAPI.Entities;

namespace OrdersAPI.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью Order.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Создать сущность.
    /// </summary>
    public Task<int> Create(Order entity);

    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    public Task<Order?> Get(Guid id);

    /// <summary>
    /// Удаление сущности.
    /// </summary>
    public Task<bool> Delete(Order entity);

    /// <summary>
    /// Изменить сущность.
    /// </summary>
    public Task<bool> Update(Order entity, UpdateOrderDto dto);

    /// <summary>
    /// Получить все записи.
    /// </summary>
    public IEnumerable<Order> GetAll();
}