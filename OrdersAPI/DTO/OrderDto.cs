using OrdersAPI.Enums;

namespace OrdersAPI.DTO;

public class OrderDto
{
    public Guid Id { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public DateTime Created { get; set; }
    
    public List<LineDto> Lines { get; set; }
}