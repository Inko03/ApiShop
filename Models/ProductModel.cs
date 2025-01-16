
using System.ComponentModel.DataAnnotations;

public class ProductModel{

    public int Id {get;set;}
    [Required(ErrorMessage ="{0} is require")]
    public string Name {get;set;}
    [Required(ErrorMessage ="{0} is require")]
    public float Price {get;set;}
    [Required(ErrorMessage ="{0} is require")]
    public string Category {get;set;}
    [Required(ErrorMessage ="{0} is require")]
    public virtual  List<ProductSize> Sizes {get;set;}
}

public class ProductSize{
    public int Id{get;set;}
    public int Size{get;set;}
    public int ProductId{get;set;}
}