public interface IOrderServices{
     Task<Orders> EditDataInDatabse(UpdataOrderStatus dto, int id);
      Task<List<Orders>> GetOrders();
     Task<Orders> GetOrder(int id);
     Task<Orders> AddOrder(Cart cart);
}