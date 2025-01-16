using System.ComponentModel.DataAnnotations;

public class User{
    public int Id{get;set;}


    [Required(ErrorMessage ="{0} is require")]
    [MinLength(3,ErrorMessage = "{0} must be longer then 3 leatter")]
    public string Name{get;set;}


    [Required(ErrorMessage ="{0} is require")]
    [EmailAddress]
    public string Email{get;set;}


    [Required(ErrorMessage = "{0} is require")]
    [MinLength(5,ErrorMessage ="{0} must be longer then 5 letters")]
    [Display(Name ="Password")]
    public string PasswordHash{get;set;}


    public string Role{get;set;}="User";


    public List<Cart> Carts {get;set;} = new();

    public DateTime RegisterDate{get;set;} = DateTime.UtcNow;
}