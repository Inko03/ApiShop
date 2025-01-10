using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiShop{

    public class ProductsControllers:ControllerBase{
        private readonly DataBaseContext _context;

        public ProductsControllers(DataBaseContext context){
            _context = context;
        }

        [HttpGet]
        [Route("/")]
        public  async Task<IActionResult> AllProducts(){
            var dane = await _context.Products
            .Include(p=>p.Sizes)
            .ToListAsync();
            return Ok(dane);
        }
        [HttpGet]
        [Route("/product/{id}")]
        public   IActionResult GetOneProduct(int id){
            var dane =  _context.Products
            .Where(p=>p.Id==id)
            .Include(p=>p.Sizes);
            return Ok(dane);
        }
        [HttpPut]
        [Route("/product/add")]
        public async Task<IActionResult> AddProductToDataBase([FromBody]ProductModel product){
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok("Dodano");
        }
        [HttpDelete]
        [Route("/product/deleted/{id}")]
        public async Task<IActionResult> DeleteProductFromDataBase(int id){
            var dane = await _context.Products
            .FirstOrDefaultAsync(p=>p.Id==id);
            _context.Products.Remove(dane);
            await _context.SaveChangesAsync();
            return Ok("Usunięto");
        }
    }

}