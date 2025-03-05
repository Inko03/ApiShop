using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiShop{
    [ApiController]
    [Route("/cart")]
    public class CartController:ControllerBase{
        private readonly ICartServices _cartServices;
        public CartController(ICartServices cartServices){
            _cartServices = cartServices;
        }

        /// <summary>
        /// Add a new shopping cart, it should be added at the final state
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> NewShopCart ([FromBody] List<CartItemDto> items){
            var result = await _cartServices.AddNewCart(items);
            if(!result.IsSuccess){
                return NotFound(result);
            }
            return Ok(result);
        }
        
        /// <summary>
        /// Return a full shopping cart
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetItems(){
            var result = await _cartServices.GetCart();
            if(!result.IsSuccess){
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}