using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CartServices:ICartServices{
    private readonly DataBaseContext _context;
    private readonly IUserContextServices _userContextServices;

    
    public CartServices(DataBaseContext context,IUserContextServices userContextServices){
        _context = context;
        _userContextServices = userContextServices;
    }

    public async Task<Result<Cart>> AddNewCart(List<CartItemDto> items){
        var userId = _userContextServices.GetUserId();

        var user = await _context.Users
        .FirstOrDefaultAsync(p=>p.Id==userId);
        if(user is null){
            return Result<Cart>.Failure("No user");
        }

        var cart = new Cart{
            UserId = userId,
            CartItems = new List<CartItem>()
        }; 
        foreach (var item in items){
            cart.CartItems.Add(new CartItem{
                ProductId = item.ProductId,
                Quantity = item.Quantity
            });
        }
        user.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return Result<Cart>.Success(cart);;
    }
    
    public async Task<Result<Cart>> GetCart(){
        var userId = _userContextServices.GetUserId(); 
        var result = await _context.Cart
        .FirstOrDefaultAsync(p=>p.UserId==userId);
        if(result is null){
            return Result<Cart>.Failure("No cart");
        }
        return Result<Cart>.Success(result);; 
    }
}