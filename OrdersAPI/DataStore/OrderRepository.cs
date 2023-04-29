using Microsoft.EntityFrameworkCore;
using OrdersAPI.Entities;
using OrdersAPI.Enums;
using OrdersAPI.Interfaces;
using OrdersAPI.Models;

namespace OrdersAPI.DataStore;

/// <inheritdoc cref="IOrderRepository"/>
public class OrderRepository : IOrderRepository
{
    private readonly OrderingContext _context;

    public OrderRepository(OrderingContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc/>
    public Order Create(Order entity)
    {
        _context.Orders.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Order? Get(Guid id)
    {
        var entity = _context.Orders
            .Include(x => x.Lines)
            .FirstOrDefault(x => x.Id == id);

        return entity;
    }

    /// <inheritdoc/>
    public bool Delete(Order entity)
    {
        var hasErrors = false;
        try
        {
            _context.Orders.Remove(entity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            hasErrors = true;
        }

        return hasErrors;
    }

    /// <inheritdoc/>
    public bool Update(Guid id, OrderStatus status)
    {
        var entity = Get(id);

        entity.Status = status;
        
        _context.SaveChanges();

        return true;
    }

    /// <inheritdoc/>
    public IEnumerable<Order> GetAll()
    {
        var baseQuery = _context.Orders
            .Include(x => x.Lines)
            .ToList();
        
        return baseQuery;
    }
    
    /// <inheritdoc/>
    public void MassCreateLines(List<LineModel> models)
    {
        foreach (var model in models)
        {
            _context.OrderLines.Add(new OrderLine
            {
                Id = model.Id,
                Quantity = model.Quantity,
                OrderId = model.OrderId
            });
            
            _context.SaveChanges();
        } 
    }

    /// <inheritdoc/>
    public void MassDeleteLines(List<Guid> idsToDelete)
    {
        foreach (var id in idsToDelete)
        {
            var entity = _context.OrderLines.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new Exception("Not found");
            }

            _context.OrderLines.Remove(entity);
            _context.SaveChanges();
        }
    }

    /// <inheritdoc/>
    public void MassUpdateLines(List<LineModel> models)
    {
        foreach (var model in models)
        {
            var entity = _context.OrderLines.First(x => x.Id == model.Id);
            entity.Quantity = model.Quantity;
            _context.SaveChanges();
        }
    }
}