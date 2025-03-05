using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiShop{
    [ApiController]
    [Route("/users")]
    public class UserController:ControllerBase{
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices){
            _userServices = userServices;
        }        

        /// <summary>
        /// Register user to database, using a standard User model 
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]User user){
            var result = await _userServices.AddUserToDatabase(user);
            if(result.IsSuccess){
                return Ok(result);
            }
                return BadRequest(result);
        }

        /// <summary>
        /// Login user and send back tocken, using a Dto model of User
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserDto user ){
                var result = await _userServices.CheckedUserInDb(user);
                if(!result.IsSuccess){
                    return Unauthorized(result);
                }
                return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo(){
            var user = await _userServices.GetUser();
            if(!user.IsSuccess){
                return BadRequest(user);
            }
            return Ok(user);
        }
    }
}