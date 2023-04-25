using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Context;
using OrdersAPI.DTO;
using OrdersAPI.Entities;
using OrdersAPI.Enums;

namespace OrdersAPI.Controllers;

[ApiController]
[Route("/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderingContext _context;

    public OrdersController(OrderingContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public IEnumerable<OrderDto> GetOrders()
    {
        var baseQuery = _context.Orders;

        var query = baseQuery
            .Select(x => new OrderDto
            {
                Id = x.Id,
                Created = x.Created,
                Status = x.Status,
                Lines = x.Lines.Select(y => new LineDto
                {
                    Id = y.Id,
                    Quantity = y.Quantity
                }).ToList()
            });

        return query;
    }

    [HttpPost("")]
    public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto dto)
    {
        var entity = new Order
        {
            Id = dto.Id,
            Created = DateTime.Now,
            Status = OrderStatus.New,
            Lines = dto.Lines.Select(x => new OrderLine
            {
                Id = x.Id,
                Quantity = x.Quantity, 
                OrderId = dto.Id
            }).ToList()
        };

        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new {id = entity.Id},entity);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Order>> UpdateOrder(Guid id, UpdateOrderDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var entity = await _context.Orders
            .Include(x => x.Lines)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
        {
            return NotFound();
        }

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

        return CreatedAtAction(nameof(GetOrder), new {id = entity.Id}, entity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteOrder(Guid id)
    {
        var entity = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(entity);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public ActionResult<OrderDto> GetOrder(Guid id)
    {
        var order = _context.Orders.FirstOrDefault(x => x.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        var orderDto = new OrderDto
        {
            Id = order.Id,
            Created = order.Created,
            Status = order.Status,
            Lines = order.Lines.Select(x => new LineDto
            {
                Id = x.Id,
                Quantity = x.Quantity
            }).ToList()
        };

        return orderDto;
    }
}