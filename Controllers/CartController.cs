using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiShop{
    [Route("/items")]
    public class CartController:ControllerBase{
        private readonly CartServices cartServices;
        public CartController(CartServices _cartServices){
            cartServices = _cartServices;
        }
        [HttpPut]
        public async Task<IActionResult> AddItem ([FromBody] List<CartItemDto> items){
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await cartServices.AddNewCart(items,id);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem([FromRoute] int id){

            return Ok($"Delete id:{id}");
        }
        [HttpGet]
        public IActionResult GetItems(){
            return Ok();
        }
    }
}