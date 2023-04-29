using OrdersAPI.Entities;
using OrdersAPI.Enums;
using OrdersAPI.Interfaces;
using OrdersAPI.Models;

namespace OrdersAPI.Services;

/// <inheritdoc cref="IOrderDeleteService"/>
public class OrderDeleteService : IOrderDeleteService
{
    private readonly IOrderRepository _repository;

    public OrderDeleteService(IOrderRepository repository)
    {
        _repository = repository;
    }
    
    /// <inheritdoc />
    public void Execute(OrderDeleteModel model)
    {
        var entity = _repository.Get(model.EntityId);
        if (entity == null)
        {
            throw new Exception("Not found");
        }
        
        Validate(entity);

        var hasErrors = _repository.Delete(entity);
        if (hasErrors)
        {
            throw new Exception("Inner exception");
        }
    }

    private void Validate(Order entity)
    {
        var closedStatuses = new List<OrderStatus>
            {OrderStatus.SentToDelivery, OrderStatus.Delivered, OrderStatus.Completed};

        if (closedStatuses.Contains(entity.Status))
        {
            throw new Exception($"Order with statuses '{string.Join(" ", closedStatuses)} is readonly'");
        }
    }
}