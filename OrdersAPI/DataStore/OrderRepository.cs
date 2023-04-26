using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.DTO;
using OrdersAPI.Entities;
using OrdersAPI.Interfaces;

namespace OrdersAPI.Context;

/// <inheritdoc cref="IOrderRepository"/>
public class OrderRepository : IOrderRepository
{
    private readonly OrderingContext _context;

    public OrderRepository(OrderingContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc/>
    public async Task<int> Create(Order entity)
    {
        _context.Orders.Add(entity);
        return await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<Order?> Get(Guid id)
    {
        var entity = await _context.Orders
            .Include(x => x.Lines)
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(Order entity)
    {
        var hasErrors = false;
        try
        {
            _context.Orders.Remove(entity);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            hasErrors = true;
        }

        return hasErrors;
    }

    /// <inheritdoc/>
    public async Task<bool> Update(Order entity, UpdateOrderDto dto)
    {
        if (dto.Status != entity.Status)
        {
            entity.Status = dto.Status;
        }

        foreach (var line in entity.Lines)
        {
            var lineDto = dto.Lines.FirstOrDefault(x => x.Id == line.Id);
            if (lineDto != null && lineDto.Quantity != line.Quantity)
            {
                line.Quantity = lineDto.Quantity;
            }
        }

        await _context.SaveChangesAsync();

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
}