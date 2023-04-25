namespace OrdersAPI.Entities;

public class OrderLine
{
    public Guid Id { get; set; }
    
    public int Quantity { get; set; }
    
    public Guid OrderId { get; set; }
}