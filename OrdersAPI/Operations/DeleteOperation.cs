using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Enums;
using OrdersAPI.Interfaces;

namespace OrdersAPI.Operations;

[ApiController]
[Route("/orders")]
public class DeleteOperation : ControllerBase
{
    private readonly IOrderRepository _repository;

    public DeleteOperation(IOrderRepository repository)
    {
        _repository = repository;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var entity = await _repository.Get(id);

        if (entity == null)
        {
            return NotFound();
        }
        
        var closedStatuses = new List<OrderStatus>
            {OrderStatus.SentToDelivery, OrderStatus.Delivered, OrderStatus.Completed};

        if (closedStatuses.Contains(entity.Status))
        {
            return BadRequest($"Order with statuses '{string.Join(" ", closedStatuses)} is readonly'");
        }

        var hasErrors = await _repository.Delete(entity);
        
        return hasErrors ? BadRequest() : Ok();
    }
}