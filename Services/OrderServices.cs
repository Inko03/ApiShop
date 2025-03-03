using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class OrderServices:IOrderServices{
    private readonly DataBaseContext _context;

    public OrderServices(DataBaseContext context){
        _context = context;
    }
    public async Task<Orders> EditDataInDatabse(UpdataOrderStatus dto, int id){
        var dane = await _context.Orders
        .FirstOrDefaultAsync(p=>p.Id==id);
        if(dane==null) throw new NullReferenceException("No product in database");
        dane.Status = dto.status;
        await _context.SaveChangesAsync();
        return dane;
    }
    public async Task<List<Orders>> GetOrders(){
        var dane = await _context.Orders
        .Where(p=>p.Id==Int32.Parse(ClaimTypes.NameIdentifier))
        .ToListAsync();
        return dane;
    }
    public async Task<Orders> GetOrder(int id){
        var dane = await _context.Orders
        .Where(o=>o.UserId==Int32.Parse(ClaimTypes.NameIdentifier))
        .FirstOrDefaultAsync(o=>o.Id==id);
        if(dane is null){
            throw new NullReferenceException("No such a order");
        }
        return dane;
    }
    public async Task<Orders> AddOrder(Cart cart){
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
        return order;
    }
}