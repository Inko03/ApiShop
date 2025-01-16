public class Cart{
public int Id{get;set;}
public int UserId{get;set;}
public DateTime DatePut{set;get;} = DateTime.UtcNow;
public virtual List<CartItem> CartItems {set;get;} = new List<CartItem>();     
public virtual User User{set;get;}
public virtual Orders Order{get;set;}
}