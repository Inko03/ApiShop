using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiShop{
    [Route("/items")]
    public class CartController:ControllerBase{
        private readonly CartServices cartServices;
        public CartController(CartServices _cartServices){
            cartServices = _cartServices;
        }
        /// <summary>
        /// Add a new shopping cart, it should be added at the final state
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> NewShopCart ([FromBody] List<CartItemDto> items){
            var result = await cartServices.AddNewCart(items);
            return Ok(result);
        }
        /// <summary>
        /// Return a full shopping cart
        /// </summary>
        [HttpGet]
        public IActionResult GetItems(){
            return Ok();
        }
    }
}