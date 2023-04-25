using OrdersAPI.Enums;

namespace OrdersAPI.DTO;

public class UpdateOrderDto
{
    public Guid Id { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public List<LineDto> Lines { get; set; }
}