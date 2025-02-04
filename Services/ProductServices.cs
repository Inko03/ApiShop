using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

public class ProductServices{
    private readonly DataBaseContext dataBaseContext;
    public ProductServices(DataBaseContext _databaseContext){
        dataBaseContext = _databaseContext;
    }
    public async Task AddProductToDatabase(ProductUploadModel product){
        var result = await dataBaseContext.Products
        .FirstOrDefaultAsync(p=>p.Name==product.Name);
        if(result!=null){
            throw new InvalidDataException("Product with that name alredy in database");
        }
        if(product.File.Length>10*1024*1024){
            
        }
        try{
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", product.File.FileName);
            using(var stream = new FileStream(filePath,FileMode.Create)){
                await product.File.CopyToAsync(stream);
            }
            var dane =new ProductModel{
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Sizes = product.Sizes,
                FilePath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads\\{product.File.FileName}"
            };
            await dataBaseContext.Products.AddAsync(dane);
            await dataBaseContext.SaveChangesAsync();
        }catch{
            throw new Exception("Same problem when adding product");
        }

    }
    public async Task DeletProduct(int id){
        var dane = await dataBaseContext.Products
        .FirstOrDefaultAsync(p=>p.Id == id);
        if(dane==null){
            throw new InvalidDataException("No data in database");
        }
        try{
            dataBaseContext.Products.Remove(dane);
            await dataBaseContext.SaveChangesAsync();
        }catch{
            throw new  Exception("Something went wrong while removing");
        }
    }
    public async Task<List<ProductModel>> GetAllProduct(){
            var dane = await dataBaseContext.Products
            .Include(p=>p.Sizes)
            .ToListAsync();
            return dane;
    }
    public async Task<ProductModel> GetOneProduct(int id){
            var dane = await  dataBaseContext.Products
            .Include(p=>p.Sizes)
            .FirstOrDefaultAsync(p=>p.Id==id);
            if(dane==null){
                throw new InvalidCastException("No product in database");
            } 
            return dane;
    }
}