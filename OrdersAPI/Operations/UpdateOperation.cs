using Microsoft.AspNetCore.Mvc;
using OrdersAPI.DTO;
using OrdersAPI.Enums;
using OrdersAPI.Interfaces;

namespace OrdersAPI.Operations;

[ApiController]
[Route("/orders")]
public class UpdateOperation : ControllerBase
{
    private readonly IOrderRepository _repository;

    public UpdateOperation(IOrderRepository repository)
    {
        _repository = repository;
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<OrderDto>> Update(Guid id, UpdateOrderDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var entity = await _repository.Get(id);
        if (entity == null)
        {
            return NotFound();
        }

        if (dto.Lines.Count == 0)
        {
            return BadRequest("Order must have order lines.");
        }
        
        if (dto.Lines.Any(x => x.Quantity < 1))
        {
            return BadRequest("The quantity must not be less than 1");
        }

        var closedStatuses = new List<OrderStatus>
            {OrderStatus.Paid, OrderStatus.SentToDelivery, OrderStatus.Delivered, OrderStatus.Completed};
        if (closedStatuses.Contains(entity.Status))
        {
            return BadRequest($"Order with statuses '{string.Join(" ", closedStatuses)} is readonly'");
        }

        if (!await _repository.Update(entity, dto))
        {
            return BadRequest();
        }

        var orderDto = new OrderDto
        {
            Id = entity.Id,
            Status = entity.Status,
            Created = entity.Created,
            Lines = entity.Lines.Select(x => new LineDto
            {
                Id = x.Id,
                Quantity = x.Quantity
            }).ToList()
        };

        return Ok(orderDto);
    }
}