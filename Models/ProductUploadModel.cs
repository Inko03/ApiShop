
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductUploadModel{

    public int Id {get;set;}


    [Required(ErrorMessage ="{0} is require")]
    public string Name {get;set;}

    [Required(ErrorMessage ="{0} is require")]
    public decimal Price {get;set;}
    
    [Required(ErrorMessage ="{0} is require")]
    public string Category {get;set;}
    [Required(ErrorMessage ="{0} is require")]
    public virtual  List<ProductSize> Sizes {get;set;}

    [NotMapped]
    [Required(ErrorMessage ="{0} is require")]
    public IFormFile File{get;set;}
}