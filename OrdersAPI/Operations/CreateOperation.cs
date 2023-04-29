using Microsoft.AspNetCore.Mvc;
using OrdersAPI.DTO;
using OrdersAPI.Entities;
using OrdersAPI.Interfaces;
using OrdersAPI.Models;

namespace OrdersAPI.Operations;

[ApiController]
[Route("/orders")]
public class CreateOperation : ControllerBase
{
    private readonly IOrderCreateService _createService;

    public CreateOperation(IOrderCreateService createService)
    {
        _createService = createService;
    }

    [HttpPost("")]
    public ActionResult<OrderDto> Create(CreateOrderDto dto)
    {
        if (dto.Id == default(Guid))
        {
            return BadRequest("Empty dto id");
        }

        if (dto.Lines.Count == 0)
        {
            return BadRequest("Empty lines");
        }

        var createModel = new OrderCreateModel
        {
            EntityId = dto.Id,
            Lines = dto.Lines
        };

        Order? entity = null;
        try
        {
            entity = _createService.Execute(createModel);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var orderDto = new OrderDto
        {
            Id = entity.Id,
            Created = entity.Created,
            Status = entity.Status.ToString(),
            Lines = entity.Lines.Select(x => new LineDto
            {
                Id = x.Id,
                Quantity = x.Quantity
            }).ToList()
        };

        return Created($"{Request.Path}/{orderDto.Id}", orderDto);
    }
}