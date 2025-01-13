public class User{
    public int Id{get;set;}
    public string? Name{get;set;}
    public string Email{get;set;}
    public string PasswordHash{get;set;}
    public string Role{get;set;}="User";
    public List<Cart> Carts {get;set;}
    public DateTime RegisterDate{get;set;}
}