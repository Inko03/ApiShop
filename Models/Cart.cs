public class Cart{
public int Id{get;set;}
public int UserId{get;set;}

public DateTime DatePut{get;} = DateTime.UtcNow;
public virtual List<CartItem> CartItems {get;}
public virtual User User{get;}
}