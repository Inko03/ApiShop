using System.Transactions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

public class ProductServices:IProductServices{
    private readonly DataBaseContext _context;
    private readonly IWebHostEnvironment _env;
    public ProductServices(DataBaseContext databaseContext,IWebHostEnvironment webHostEnvironment){
        _context = databaseContext;
        _env = webHostEnvironment;
    }

    private void CheckFileSize(double size){
        if(size>10*1024*1024){
            throw new InvalidOperationException("File was too big");
        }
    }
    private  string GetFilePath(string fileName){
        var uploadsPath = Path.Combine(_env.WebRootPath,"uploads");
        if(!Directory.Exists(uploadsPath)){
            Directory.CreateDirectory(uploadsPath);
        }
        var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","uploads",fileName);
        return filePath;
    }
    public async Task<Result<bool>> AddProductToDatabase(ProductUploadModel product){
        var result = await _context.Products
        .FirstOrDefaultAsync(p=>p.Name==product.Name);
        if(result is not null){
            return Result<bool>.Failure("That product already is in the databse");
        }
        CheckFileSize(product.File.Length);
        try{
            var filePath = GetFilePath(product.File.FileName);
            using(var stream = new FileStream(filePath,FileMode.Create)){
                await product.File.CopyToAsync(stream);
            }
            var dane =new ProductModel{
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Sizes = product.Sizes,
                FilePath = filePath
            };
            await _context.Products.AddAsync(dane);
            await _context.SaveChangesAsync();
        }catch(Exception ex){
            return Result<bool>.Failure($"{ex}");
        }
        return Result<bool>.Success(true);

    }
    private void FindFileAndDelete(string path){
        if(System.IO.File.Exists(path)){
            System.IO.File.Delete(path);
        }
        throw new Exception("No file to remove");
    }
    public async Task<Result<bool>> DeletProduct(int id){
        var dane = await _context.Products
        .FirstOrDefaultAsync(p=>p.Id == id);
        if(dane==null){
            return Result<bool>.Failure("No product in database");
        }

        try
        {
            FindFileAndDelete(dane.FilePath);
            _context.Products.Remove(dane);
            await _context.SaveChangesAsync();
        }catch(Exception e){
            throw new  Exception(e.Message);
        }
        return Result<bool>.Success(true);
    }
    public async Task<Result<List<ProductModel>>> GetAllProduct(){
            var dane = await _context.Products
            .Include(p=>p.Sizes)
            .ToListAsync();
            if(dane==null){
                Result<List<ProductModel>>.Failure("No product in database");
            }
            return Result<List<ProductModel>>.Success(dane);;
    }
    public async Task<Result<ProductModel>> GetOneProduct(int id){
            var dane = await  _context.Products
            .Include(p=>p.Sizes)
            .FirstOrDefaultAsync(p=>p.Id==id);
            if(dane== null){
                return Result<ProductModel>.Failure("No sucha product in database");
            }
        return Result<ProductModel>.Success(dane);
    }
    public async Task<Result<ProductModel>> Edit(ProductUploadModel product,int id){
        if(product.Price==0){
            return Result<ProductModel>.Failure("Price is require");
        }
        CheckFileSize(product.File.Length);
        var filePath = GetFilePath(product.File.FileName);
        var productDb = await _context.Products.Include(p=>p.Sizes).FirstOrDefaultAsync(o=>o.Id==id);
        if(productDb==null){
            return Result<ProductModel>.Failure("No product in databse");
        }
        try{
            _context.ProductSize.RemoveRange(productDb.Sizes);
            productDb.Name=product.Name;
            productDb.Price=product.Price;
            productDb.FilePath=filePath;
            productDb.Category = product.Category;
            productDb.Sizes=product.Sizes;
            await _context.SaveChangesAsync();
            using(var stream = new FileStream(filePath,FileMode.Create)){
                    await product.File.CopyToAsync(stream);
            }
        }catch(Exception){
            return Result<ProductModel>.Failure("Same problem when saving to database");
        }
        return Result<ProductModel>.Success(productDb);
    }
}