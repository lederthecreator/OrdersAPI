using Microsoft.AspNetCore.Mvc;
using OrdersAPI.DTO;
using OrdersAPI.Interfaces;

namespace OrdersAPI.Operations;

[ApiController]
[Route("/orders/list")]
public class ListOperation : ControllerBase
{
    private readonly IOrderRepository _repository;

    public ListOperation(IOrderRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    public IEnumerable<OrderDto> List()
    {
        var baseQuery = _repository.GetAll();
        
        return baseQuery
            .Select(x => new OrderDto
            {
                Id = x.Id,
                Status = x.Status,
                Created = x.Created,
                Lines = x.Lines.Select(y => new LineDto
                {
                    Id = y.Id,
                    Quantity = y.Quantity
                }).ToList()
            })
            .OrderByDescending(x => x.Created)
            .ToList();
    }
}