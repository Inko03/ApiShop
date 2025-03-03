public interface IMessageServices{
 object Message(string errorStatus, string errorMessage);
 object DataSender(string errorStatus,List<ProductModel> product);
}