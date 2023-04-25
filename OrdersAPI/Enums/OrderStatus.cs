namespace OrdersAPI.Enums;

public enum OrderStatus
{
    New = 10, 
    
    WaitingToPayment = 20, 
    
    Paid = 30, 
    
    SentToDelivery = 40, 
    
    Delivered = 50, 
    
    Completed = 60
}