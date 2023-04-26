using Microsoft.AspNetCore.Mvc;
using OrdersAPI.DTO;
using OrdersAPI.Entities;
using OrdersAPI.Enums;
using OrdersAPI.Interfaces;

namespace OrdersAPI.Operations;

[ApiController]
[Route("/orders")]
public class CreateOperation : ControllerBase
{
    private readonly IOrderRepository _repository;

    public CreateOperation(IOrderRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("")]
    public async Task<ActionResult<OrderDto>> Create(CreateOrderDto dto)
    {
        if (dto.Id == default(Guid))
        {
            return BadRequest("Empty dto id");
        }

        if (dto.Lines.Count == 0)
        {
            return BadRequest("Empty lines");
        }

        if (dto.Lines.Any(x => x.Quantity < 1))
        {
            return BadRequest("The quantity must not be less than 1");
        }

        var entity = new Order
        {
            Id = dto.Id,
            Created = DateTime.Now,
            Status = OrderStatus.New,
            Lines = dto.Lines.Select(x => new OrderLine
            {
                Id = x.Id,
                Quantity = x.Quantity
            }).ToList()
        };

        await _repository.Create(entity);

        return Created($"{Request.Path}/{entity.Id}", entity);
    }
}