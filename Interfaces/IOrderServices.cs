public interface IOrderServices{
     Task<Result<Orders>> EditDataInDatabse(UpdataOrderStatus dto, int id);
     Task<Result<List<Orders>>> GetOrders();
     Task<Result<Orders>> GetOrder(int id);
     Task<Result<Orders>> AddOrder(Cart cart);
}