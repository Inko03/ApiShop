using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiShop{
    public class UserController:ControllerBase{

        public IActionResult RegisterUser([FromBody]User user){
            return Ok() ;
        }
        public IActionResult LoginUser( ){
            return Ok();
        }

    }
}