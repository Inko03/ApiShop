public interface IProductServices{
    Task AddProductToDatabase(ProductUploadModel product);
    Task DeletProduct(int id);
    Task<List<ProductModel>> GetAllProduct();
    Task<ProductModel> GetOneProduct(int id);
}