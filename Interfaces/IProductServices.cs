public interface IProductServices{
    Task<Result<bool>> AddProductToDatabase(ProductUploadModel product);
    Task<Result<bool>> DeletProduct(int id);
    Task<Result<List<ProductModel>>> GetAllProduct();
    Task<Result<ProductModel>> GetOneProduct(int id);
    Task<Result<ProductModel>> Edit(ProductUploadModel product,int id);
}