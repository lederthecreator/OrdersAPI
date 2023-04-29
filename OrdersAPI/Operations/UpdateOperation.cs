using Microsoft.AspNetCore.Mvc;
using OrdersAPI.DTO;
using OrdersAPI.Interfaces;
using OrdersAPI.Models;

namespace OrdersAPI.Operations;

[ApiController]
[Route("/orders")]
public class UpdateOperation : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly IOrderUpdateService _updateService;

    public UpdateOperation(
        IOrderRepository repository, 
        IOrderUpdateService updateService)
    {
        _repository = repository;
        _updateService = updateService;
    }

    [HttpPut("{id:guid}")]
    public ActionResult<OrderDto> Update(Guid id, UpdateOrderDto dto)
    {
        var entity = _repository.Get(id);
        if (entity == null)
        {
            return NotFound();
        }

        if (dto.Lines.Count == 0)
        {
            return BadRequest("Order must have order lines.");
        }

        try
        {
            var updateModel = new OrderUpdateModel
            {
                EntityId = entity.Id,
                Status = dto.Status,
                Lines = dto.Lines.Select(x => new LineModel
                {
                    Id = x.Id, 
                    Quantity = x.Quantity,
                    OrderId = entity.Id
                }).ToList()
            };

            _updateService.Execute(updateModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var orderDto = new OrderDto
        {
            Id = entity.Id,
            Status = entity.Status.ToString(),
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