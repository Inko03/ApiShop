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

        [HttpGet]
        public async  Task<IActionResult> AllProducts(){
            var dane =  await _product.GetAllProduct();
            return Ok(_messageServices.DataSender("none", dane));
        }
        [HttpGet("{id}")]
        public async  Task<IActionResult> GetOneProduct(int id){
            var dane = await _product.GetOneProduct(id);
            return Ok(new{status="succes",data=dane});
        }
        //Only admin shuld add a item
        [HttpPost("add")]
        public async Task<IActionResult> AddProductToDataBase([FromForm]ProductUploadModel product){
            if(product.File==null){
                return BadRequest(_messageServices.Message("error","No file here"));
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", product.File.FileName);
            using(var stream = new FileStream(filePath,FileMode.Create)){
                await product.File.CopyToAsync(stream);
            }
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductFromDataBase(int id){
            //if product exist is checkd first in here
            await _product.DeletProduct(id);
            return NoContent();
        }
    }

}