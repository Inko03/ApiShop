public interface ICartServices{
         Task<User> AddNewCart(List<CartItemDto> items);
         Task<Cart> GetCart();
}