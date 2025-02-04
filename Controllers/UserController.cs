using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiShop{
    [Route("/users")]
    public class UserController:ControllerBase{
        private readonly UserServices userServices;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly MessageServices _messageServices;
        public UserController(UserServices _userServices,MessageServices messageServices, IHttpContextAccessor _contextAccesor){
            userServices = _userServices;
            contextAccessor = _contextAccesor;
            _messageServices = messageServices;
        }
        [HttpPut]
        public async Task<IActionResult> RegisterUser([FromBody]User user){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(_messageServices.Message("error login",state));
            }
            var result = await userServices.AddUserToDatabase(user);
            if(result){
                return Ok(_messageServices.Message("none","User register"));
            }
            return BadRequest(_messageServices.Message("error","Some error"));
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] UserDto user ){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(_messageServices.Message("login error", state));
            }
                var result = await userServices.CorrectPassword(user);
                var cookieOptions = new CookieOptions{
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                };
                contextAccessor.HttpContext?.Response.Cookies.Append("AuthToken", $"{result}", cookieOptions);
                return Ok(_messageServices.Message("none", "User login"));
        }
        [HttpPost("logout")]
        public async Task LogOut(){
            contextAccessor.HttpContext.Response.Cookies.Append("AuthToken","",new CookieOptions{
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Path = "/"
            });
        }
    }
}