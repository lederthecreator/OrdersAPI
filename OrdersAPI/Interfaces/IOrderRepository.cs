using Microsoft.AspNetCore.Mvc;
using OrdersAPI.DTO;
using OrdersAPI.Entities;
using OrdersAPI.Enums;
using OrdersAPI.Models;

namespace OrdersAPI.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью Order.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Создать сущность.
    /// </summary>
    public Order Create(Order entity);

    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    public Order? Get(Guid id);

    /// <summary>
    /// Удаление сущности.
    /// </summary>
    public bool Delete(Order entity);

    /// <summary>
    /// Изменить сущность.
    /// </summary>
    public bool Update(Guid id, OrderStatus status);

    /// <summary>
    /// Получить все записи.
    /// </summary>
    public IEnumerable<Order> GetAll();

    /// <summary>
    /// Массовое создание связанных сущностей.
    /// </summary>
    public void MassCreateLines(List<LineModel> models);

    /// <summary>
    /// Массовое удаление связанных сущностей.
    /// </summary>
    public void MassDeleteLines(List<Guid> idsToDelete);

    /// <summary>
    /// Массовое редактирование связанных сущностей.
    /// </summary>
    public void MassUpdateLines(List<LineModel> models);
}