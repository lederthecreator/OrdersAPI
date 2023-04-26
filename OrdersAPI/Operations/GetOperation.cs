﻿using Microsoft.AspNetCore.Mvc;
using OrdersAPI.DTO;
using OrdersAPI.Interfaces;

namespace OrdersAPI.Operations;

[ApiController]
[Route("/orders")]
public class GetOperation : ControllerBase
{
    private readonly IOrderRepository _repository;

    public GetOperation(IOrderRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> Get(Guid id)
    {
        var entity = await _repository.Get(id);

        if (entity == null)
        {
            return NotFound();
        }

        var orderDto = new OrderDto
        {
            Id = entity.Id,
            Created = entity.Created,
            Status = entity.Status,
            Lines = entity.Lines.Select(x => new LineDto
            {
                Id = x.Id,
                Quantity = x.Quantity
            }).ToList()
        };

        return orderDto;
    }
}