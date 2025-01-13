using Microsoft.AspNetCore.Mvc;

namespace ApiShop{
    public class CartController:ControllerBase{
        private readonly DataBaseContext dataBaseContext;

        public CartController(DataBaseContext _dataBaseContext){
            dataBaseContext = _dataBaseContext;
        }
        [HttpPut]
        [Route("/items")]
        public IActionResult AddItem ([FromBody] CartItemDto item){
            var dane = new Cart{
                UserId = 1
            };
            dane.CartItems.Add(new CartItem{
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }
            );
            dataBaseContext.Cart.AddAsync(dane);
            return Ok(item);
        }
        [HttpDelete]
        [Route("/items/{id}")]
        public IActionResult DeleteItem([FromRoute] int id){

            return Ok($"Delete id:{id}");
        }
        [HttpGet]
        [Route("/items")]
        public IActionResult GetItems(){
            return Ok();
        }
    }
}