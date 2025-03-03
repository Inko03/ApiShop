using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CartServices:ICartServices{
    private readonly DataBaseContext _context;
    
    public CartServices(DataBaseContext context){
        _context = context;
    }

    public async Task<User> AddNewCart(List<CartItemDto> items){
        var user = await _context.Users
        .FirstOrDefaultAsync(p=>p.Id==Int32.Parse(ClaimTypes.NameIdentifier));
        if(user is null)throw new UnauthorizedAccessException("No user in database");

        var cart = new Cart{
            UserId = Int32.Parse(ClaimTypes.NameIdentifier)
        }; 
        foreach (var item in items){
            cart.CartItems.Add(new CartItem{
                ProductId = item.ProductId,
                Quantity = item.Quantity
            });
        }
        user.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return user;
    }
    
    public async Task<Cart> GetCart(){
        var result = await _context.Cart
        .FirstOrDefaultAsync(p=>p.UserId==Int32.Parse(ClaimTypes.NameIdentifier));
        if(result is null){
            throw new NullReferenceException("No such a cart");
        }
        return result; 
    }
}