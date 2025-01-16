using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiShop{
    [ApiController]
    [Route("/users")]
    public class UserController:ControllerBase{
        private readonly UserServices userServices;
        private readonly TokenServices tokenServices;
        private readonly IHttpContextAccessor contextAccessor;
        public UserController(UserServices _userServices,TokenServices _tokenServices, IHttpContextAccessor _contextAccesor){
            userServices = _userServices;
            tokenServices = _tokenServices;
            contextAccessor = _contextAccesor;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]User user){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(state);
            }
            var result = await userServices.AddUserToDatabase(user);
            if(result){
                return Ok("Account was created");
            }
            return BadRequest("Something went wrong");
        }
        [HttpPost("in")]
        public async Task<IActionResult> LoginUser([FromBody] UserDto user ){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(state);
            }
                var result = await userServices.CorrectPassword(user);
                var cookieOptions = new CookieOptions{
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                };
                contextAccessor.HttpContext?.Response.Cookies.Append("AuthToken", $"{result}", cookieOptions);
                return Ok(result);
        }
    }
}