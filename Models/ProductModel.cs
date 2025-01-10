
public class ProductModel{

    public int Id {get;set;}
    public string Name {get;set;}
    public float Price {get;set;}
    public string Category {get;set;}
    public virtual  List<ProductSize> Sizes {get;set;}
}

public class ProductSize{
    public int Id{get;set;}
    public int Size{get;set;}
    public int ProductId{get;set;}
    public virtual ProductModel ProductModel {get;set;}
}