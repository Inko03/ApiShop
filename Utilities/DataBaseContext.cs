using Microsoft.EntityFrameworkCore;

public class DataBaseContext:DbContext{

public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options){}

public DbSet<ProductModel> Products {get;set;}
public DbSet<ProductSize> ProductSize {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>()
        .HasMany(p=>p.Sizes)
        .WithOne(p=>p.ProductModel)
        .HasForeignKey(p=>p.ProductId);
    }
}