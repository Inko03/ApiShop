public class CartItem{
    public int Id{get;set;}
    public int CartId{get;set;}
    public int ProductId{get;set;}
    public int Quantity{get;set;}

    public virtual Cart Cart{get;}
    public virtual ProductModel Product{get;}
}