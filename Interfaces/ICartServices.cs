public interface ICartServices{
         Task<Result<Cart>>  AddNewCart(List<CartItemDto> items);
         Task<Result<Cart>> GetCart();
}