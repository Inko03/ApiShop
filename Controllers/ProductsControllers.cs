using Microsoft.AspNetCore.Mvc;

namespace ApiShop{

    public class ProductsControllers:ControllerBase{
        [HttpGet]
        [Route("/")]
        public  IActionResult AllProducts(){
            return Ok("All");
        }
        [HttpGet]
        [Route("/product/{id}")]
        public  IActionResult GetOneProduct(int id){
            return Ok($"One {id}");
        }
        [HttpPut]
        [Route("/product/add")]
        public IActionResult AddProductToDataBase([FromBody]ProductModel product){

            return Ok(product);
        }
        [HttpDelete]
        [Route("/product/deleted/{id}")]
        public IActionResult DeleteProductFromDataBase(int id){
            return Ok($"Product deleted {id}");
        }
    }

}