using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Interfaces;
using OrdersAPI.Models;

namespace OrdersAPI.Operations;

[ApiController]
[Route("/orders")]
public class DeleteOperation : ControllerBase
{
    private readonly IOrderDeleteService _deleteService;

    public DeleteOperation(IOrderDeleteService deleteService)
    {
        _deleteService = deleteService;
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        var deleteModel = new OrderDeleteModel
        {
            EntityId = id
        };

        try
        {
            _deleteService.Execute(deleteModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}