namespace OrdersAPI.DTO;

public class CreateOrderDto
{
    public Guid Id { get; set; }
    public List<LineDto> Lines { get; set; }
}