using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiShop{
[ApiController]
[Route("products")]
    public class ProductsController:ControllerBase{
        private readonly IProductServices _productService;
        public ProductsController(IProductServices product){
            _productService = product;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        public async  Task<IActionResult> GetProducts(){
            var dane =  await _productService.GetAllProduct();
            if(!dane.IsSuccess){
                return BadRequest(dane);
            }
            return Ok(dane);
        }

        /// <summary>
        /// Get one product
        /// </summary>
        [HttpGet("{id}")]
        public async  Task<IActionResult> GetProduct(int id){
            var result = await _productService.GetOneProduct(id);
            if(!result.IsSuccess){
                return NotFound(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Adding product to databse, only admin 
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm]ProductUploadModel product){
            var result = await _productService.AddProductToDatabase(product);
            if(!result.IsSuccess){
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete product from database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductFromDataBase(int id){
            var result = await _productService.DeletProduct(id);
            if(!result.IsSuccess){
               return NotFound(result);
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct([FromForm]ProductUploadModel product,[FromRoute]int id){
            var result =  await _productService.Edit(product,id);
            if(!result.IsSuccess){
               return NotFound(result);
            }
            return Ok(result);
        }
    }

}
