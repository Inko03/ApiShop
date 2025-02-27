using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiShop{
    [Route("/users")]
    public class UserController:ControllerBase{
        private readonly UserServices _userServices;
        private readonly MessageServices _messageServices;
        public UserController(UserServices userServices,MessageServices messageServices){
            _userServices = userServices;
            _messageServices = messageServices;
        }        
        /// <summary>
        /// Register user to database, using a standard User model 
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> RegisterUser([FromBody]User user){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(_messageServices.Message("error",state));
            }
            var result = await _userServices.AddUserToDatabase(user);
            if(result){
                return Ok(_messageServices.Message("none","User register"));
            }
            return BadRequest(_messageServices.Message("error","Some error"));
        }
        /// <summary>
        /// Login user and send back tocken to frontend, using a Dto model of User
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] UserDto user ){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(_messageServices.Message("login error", state));
            }
                var token = await _userServices.CheckedUserInDb(user);
                return Ok(_messageServices.Message("token", $"{token}"));
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task LogOut(){
        }
    }
}