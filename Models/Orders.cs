public class Orders{
    public int Id{get;set;}
    public int UserId{get;set;}
    public int  CartId{get;set;}
    public OrderStatus Status{get;set;} = OrderStatus.Pending;
    public double TotalAmount{get;set;}
    public DateTime DatePut{get;set;}

    public virtual Cart Cart{get;set;}
}


public enum OrderStatus{
    Pending,
    Shipped,
    Completed,
    Cancelled

}