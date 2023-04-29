using OrdersAPI.Entities;
using OrdersAPI.Enums;
using OrdersAPI.Interfaces;
using OrdersAPI.Models;

namespace OrdersAPI.Services;

/// <inheritdoc cref="IOrderCreateService"/>
public class OrderCreateService : IOrderCreateService
{
    private readonly IOrderRepository _repository;

    public OrderCreateService(IOrderRepository repository)
    {
        _repository = repository;
    }
    
    /// <inheritdoc />
    public Order Execute(OrderCreateModel model)
    {
        Validate(model);
        
        var entity = new Order
        {
            Id = model.EntityId,
            Status = OrderStatus.New,
            Created = DateTime.Now,
            Lines = model.Lines.Select(x => new OrderLine
            {
                Id = x.Id,
                Quantity = x.Quantity, 
                OrderId = model.EntityId 
            }).ToList()
        };

        return _repository.Create(entity);
    }

    private void Validate(OrderCreateModel model)
    {
        if (model.Lines.Any(x => x.Quantity < 1))
        {
            throw new Exception("Quantity must be greather than 1");
        }
    }
}