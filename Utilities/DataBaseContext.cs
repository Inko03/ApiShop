using Microsoft.EntityFrameworkCore;

public class DataBaseContext:DbContext{

public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options){}

public DbSet<ProductModel> Products {get;set;}
public DbSet<ProductSize> ProductSize {get;set;}
public DbSet<Cart> Cart {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>()
        .HasMany(p=>p.Sizes)
        .WithOne(p=>p.ProductModel)
        .HasForeignKey(p=>p.ProductId);

        modelBuilder.Entity<Cart>()
        .HasMany(p=>p.CartItems)
        .WithOne(p=>p.Cart)
        .HasForeignKey(p=>p.CartId);

        modelBuilder.Entity<Cart>()
        .HasOne(p=>p.User)
        .WithMany(p=>p.Carts)
        .HasForeignKey(p=>p.UserId);
        
    }
}