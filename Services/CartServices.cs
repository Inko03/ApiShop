using Microsoft.EntityFrameworkCore;

public class CartServices{
    private readonly DataBaseContext context;

    public CartServices(DataBaseContext _context){
        context = _context;
    }

    public async Task<User> AddNewCart(List<CartItemDto> items, int id){
        var user = await context.Users
        .FirstOrDefaultAsync(p=>p.Id==id);
        if(user==null)throw new UnauthorizedAccessException("No user in database");

        var cart = new Cart{
            UserId = id
        }; 
        foreach (var item in items){
            cart.CartItems.Add(new CartItem{
                ProductId = item.ProductId,
                Quantity = item.Quantity
            });
        }
        user.Carts.Add(cart);
        await context.SaveChangesAsync();
        return user;
    }
}