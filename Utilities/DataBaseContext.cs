using Microsoft.EntityFrameworkCore;

public class DataBaseContext:DbContext{

public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options){}

public DbSet<ProductModel> Products {get;set;}
public DbSet<ProductSize> ProductSize {get;set;}
public DbSet<Cart> Cart {get;set;}
public DbSet<User> Users{get;set;}
public DbSet<Orders> Orders{get;set;}
public DbSet<CartItem> cartItems{get;set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>()
        .HasMany(p=>p.Sizes)
        .WithOne()
        .HasForeignKey(p=>p.ProductId);

        modelBuilder.Entity<Cart>()
        .HasMany(p=>p.CartItems)
        .WithOne()
        .HasForeignKey(p=>p.CartId);

        modelBuilder.Entity<Cart>()
        .HasOne(p=>p.User)
        .WithMany()
        .HasForeignKey(p=>p.UserId);
        
    }
}