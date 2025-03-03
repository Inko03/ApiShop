using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiShop{
[ApiController]
[Route("products")]
    public class ProductsController:ControllerBase{
        private readonly IProductServices _product;
        private readonly IMessageServices _messageServices;
        public ProductsController(IProductServices product, IMessageServices messageServices){
            _product = product;
            _messageServices = messageServices;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        public async  Task<IActionResult> GetProducts(){
            var dane =  await _product.GetAllProduct();
            return Ok(_messageServices.DataSender("succes", dane));
        }

        /// <summary>
        /// Get one product
        /// </summary>
        [HttpGet("{id}")]
        public async  Task<IActionResult> GetProduct(int id){
            var dane = await _product.GetOneProduct(id);
            return Ok(new{status="succes", data=dane});
        }

        /// <summary>
        /// Adding product to databse, only admin 
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm]ProductUploadModel product){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(_messageServices.Message("error", state));
            }
            await _product.AddProductToDatabase(product);
            return Ok(_messageServices.Message("created", product.Name));
        }

        /// <summary>
        /// Delete product from database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductFromDataBase(int id){
            await _product.DeletProduct(id);
            return NoContent();
        }
    }

}
