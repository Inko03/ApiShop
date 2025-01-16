using Microsoft.EntityFrameworkCore;

public class OrderServices{
    private readonly DataBaseContext context;

    public OrderServices(DataBaseContext _context){
        context = _context;
    }
    public async Task<Orders> EditDataInDatabse(UpdataOrderStatus dto, int id){
        var dane = await context.Orders
        .FirstOrDefaultAsync(p=>p.Id==id);
        if(dane==null) throw new NullReferenceException("No product in database");
        dane.Status = dto.status;
        await context.SaveChangesAsync();
        return dane;
    }
    public async Task<Orders> GetAllOrdersById(int id){
        var dane = await context.Orders
        .FirstOrDefaultAsync(p=>p.Id==id);
        if(dane==null){
            throw new NullReferenceException("No orders");
        }
        return dane;
    }
    public async Task<Orders> AddOrder(Cart cart){
        var items = cart.CartItems;
        double totalPrice = 0;
        foreach (var item in items)
        {
            totalPrice = Math.Round(context.Products
            .Where(p=>p.Id==item.ProductId)
            .Sum(p=>p.Price*item.Quantity),2);
        }
        var order = new Orders{
            UserId = cart.UserId,
            CartId = cart.Id,
            TotalAmount = totalPrice,
            DatePut = cart.DatePut
        };
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        return order;
    }
}