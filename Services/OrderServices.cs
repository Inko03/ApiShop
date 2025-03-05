using System.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class OrderServices:IOrderServices{
    private readonly DataBaseContext _context;
     private readonly IUserContextServices _userContextServices;
    public OrderServices(DataBaseContext context,IUserContextServices userContextServices){
        _context = context;
        _userContextServices = userContextServices;
    }
    public async Task<Result<Orders>> EditDataInDatabse(UpdataOrderStatus dto, int id){
        var dane = await _context.Orders
        .FirstOrDefaultAsync(p=>p.Id==id);
        if(dane==null){
            return Result<Orders>.Failure("No data in database");
        }
        dane.Status = dto.status;
        await _context.SaveChangesAsync();
        return Result<Orders>.Success(dane);
    }
    public async Task<Result<List<Orders>>> GetOrders(){
        var userId = _userContextServices.GetUserId();
        var dane = await _context.Orders
        .Where(p=>p.UserId==userId)
        .ToListAsync();
        if(dane.Count<1){
            return Result<List<Orders>>.Failure("No orders");
        }
        return Result<List<Orders>>.Success(dane);
    }
    public async Task<Result<Orders>> GetOrder(int id){
        var userId = _userContextServices.GetUserId();
        var dane = await _context.Orders
        .Where(o=>o.UserId==userId)
        .FirstOrDefaultAsync(o=>o.Id==id);
        if(dane is null){
            return Result<Orders>.Failure("No sucha order");
        }
        return Result<Orders>.Success(dane);;
    }
    public async Task<Result<Orders>> AddOrder(Cart cart){
        var items = cart.CartItems;
        decimal totalPrice = 0;
        foreach (var item in items)
        {
        var total = _context.Products
            .Where(p => p.Id == item.ProductId)
            .Select(p => (decimal)p.Price * (decimal)item.Quantity)
            .ToList();

        totalPrice = Math.Round(total.Sum(), 2);
        }
        var order = new Orders{
            UserId = cart.UserId,
            CartId = cart.Id,
            TotalAmount = totalPrice,
            DatePut = cart.DatePut
        };
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return Result<Orders>.Success(order);
    }
}