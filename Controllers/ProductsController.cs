using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiShop{
[ApiController]
[Route("products")]
    public class ProductsController:ControllerBase{
        private readonly ProductServices _product;
        public ProductsController(ProductServices product){
            _product = product;
        }

        [HttpGet]
        public async  Task<IActionResult> AllProducts(){
            var dane =  await _product.GetAllProduct();
            return Ok(dane);
        }
        [HttpGet("{id}")]
        public async  Task<IActionResult> GetOneProduct(int id){
            var dane = await _product.GetOneProduct(id);
            return Ok(dane);
        }
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProductToDataBase([FromBody]ProductModel product){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(state);
            }
            await _product.AddProductToDatabase(product);
            return CreatedAtAction(nameof(GetOneProduct),new{id=product.Id,Name=product.Name});
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductFromDataBase(int id){
            //if product exist is checkd first in here
            await _product.DeletProduct(id);
            return NoContent();
        }
    }

}