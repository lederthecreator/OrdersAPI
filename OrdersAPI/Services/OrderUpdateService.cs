using System.Collections.Immutable;
using OrdersAPI.Entities;
using OrdersAPI.Enums;
using OrdersAPI.Interfaces;
using OrdersAPI.Models;

namespace OrdersAPI.Services;

/// <inheritdoc cref="IOrderUpdateService"/>
public class OrderUpdateService  : IOrderUpdateService
{
    private readonly IOrderRepository _repository;

    public OrderUpdateService(IOrderRepository repository)
    {
        _repository = repository;
    }
    
    /// <inheritdoc />
    public void Execute(OrderUpdateModel model)
    {
        Validate(model);
        
        var internalModel = ProcessLines(model);
        
        SaveChanges(internalModel, model);
    }

    private void Validate(OrderUpdateModel model)
    {
        var entity = _repository.Get(model.EntityId);
        if (entity == null)
        {
            throw new Exception("Not found");
        }

        var errorList = new List<string>();

        var closedStatuses = new List<OrderStatus>
            {OrderStatus.Paid, OrderStatus.SentToDelivery, OrderStatus.Delivered, OrderStatus.Completed};
        if (closedStatuses.Contains(entity.Status))
        {
            errorList.Add($"Order with statuses '{string.Join(" ", closedStatuses)} is readonly'");
        }

        foreach (var line in model.Lines)
        {
            if (line.Quantity < 1)
            {
                errorList.Add($"LineID: {line.Id}, quantity less than 1");
            }
        }

        if (errorList.Count > 0)
        {
            throw new Exception(string.Join("\n", errorList));
        }
    }

    private InternalModel ProcessLines(OrderUpdateModel model)
    {
        var internalModel = new InternalModel();

        var entity = _repository.Get(model.EntityId);
        internalModel.Order = entity;
        var entityLines = entity.Lines
            .Select(x => new LineModel
            {
                Id = x.Id, 
                Quantity = x.Quantity, 
                OrderId = entity.Id
            })
            .ToList();
        
        // Заполним строки к обновлению
        foreach (var modelLine in model.Lines)
        {
            if (entityLines.Any(x => x.Id == modelLine.Id))
            {
                var entityLine = entityLines.First(x => x.Id == modelLine.Id);
                if (entityLine.Quantity != modelLine.Quantity)
                {
                    entityLine.Quantity = modelLine.Quantity;
                    internalModel.LineToUpdateModels.Add(modelLine);
                }
            }
            else
            {
                internalModel.LineToCreateModels.Add(modelLine);
            }
        }

        // Заполним строки к удалению
        foreach (var entityLine in entityLines)
        {
            if (model.Lines.All(x => x.Id != entityLine.Id))
            {
                internalModel.LineToDeleteIds.Add(entityLine.Id);
            }
        }

        return internalModel;
    }

    private void SaveChanges(InternalModel model, OrderUpdateModel updateModel)
    {
        if (model.Order != null)
        {
            var status = Enum.Parse<OrderStatus>(updateModel.Status);
            _repository.Update(updateModel.EntityId, status);
        }

        if (model.LineToUpdateModels.Count != 0)
        {
            _repository.MassUpdateLines(model.LineToUpdateModels);
        }

        if (model.LineToDeleteIds.Count != 0)
        {
            _repository.MassDeleteLines(model.LineToDeleteIds);
        }
        
        if (model.LineToCreateModels.Count != 0)
        {
            _repository.MassCreateLines(model.LineToCreateModels);
        }
        
    }

    private class InternalModel
    {
        public Order? Order { get; set; }
        
        public List<Guid> LineToDeleteIds { get; set; } = new();

        public List<LineModel> LineToUpdateModels { get; set; } = new();

        public List<LineModel> LineToCreateModels { get; set; } = new();
    }
}