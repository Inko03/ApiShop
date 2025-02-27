using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiShop{
[ApiController]
[Route("products")]
    public class ProductsController:ControllerBase{
        private readonly ProductServices _product;
        private readonly MessageServices _messageServices;
        public ProductsController(ProductServices product, MessageServices messageServices){
            _product = product;
            _messageServices = messageServices;
        }
        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        public async  Task<IActionResult> AllProducts(){
            var dane =  await _product.GetAllProduct();
            return Ok(_messageServices.DataSender("none", dane));
        }
        /// <summary>
        /// Get one product
        /// </summary>
        [HttpGet("{id}")]
        public async  Task<IActionResult> GetOneProduct(int id){
            var dane = await _product.GetOneProduct(id);
            return Ok(new{status="succes",data=dane});
        }
        /// <summary>
        /// Adding product to databse, only admin 
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddProductToDataBase([FromForm]ProductUploadModel product){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(_messageServices.Message("error",state));
            }
            await _product.AddProductToDatabase(product);
            return Ok(_messageServices.Message("created",product.Name));
        }
        /// <summary>
        /// Delete product from database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductFromDataBase(int id){
            //if product exist is checkd first in here
            await _product.DeletProduct(id);
            return NoContent();
        }
    }

}
