

using System.ComponentModel.DataAnnotations.Schema;

public class ProductModel{

    public int Id {get;set;}
    public string Name {get;set;}
    public float Price {get;set;}
    public string Category {get;set;}
    public List<ProdcutSize> Sizes {get;set;}
}

public class ProdcutSize{
    public int Id{get;set;}
    public int Size{get;set;}
    public int ProductId{get;set;}
    [ForeignKey("ProductModel")]
    public ProductModel ProductModel {get;set;}
}