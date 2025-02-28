using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("/order")]
public class OrderController:ControllerBase{
    private readonly OrderServices _orderServices;
    private readonly MessageServices _messageServices;
    public OrderController(OrderServices orderServices,MessageServices messageServices){
        _orderServices = orderServices;
        _messageServices = messageServices;
    }
        /// <summary>
        /// Add new order, only if payment will be succesfull
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Cart cart){
                var result = await _orderServices.AddOrder(cart);
                return Ok(result);
        }
        /// <summary>
        /// Return one specific order
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id){
            var result = await _orderServices.GetOrder(id);
            return Ok(result);
        }
        /// <summary>
        /// Return all user orders
        /// </summary>
        [Authorize]
        [HttpGet("/orders")]
        public async Task<IActionResult> GetOrders(){
            var result = await _orderServices.GetOrders();
            return Ok(result);
        }
        /// <summary>
        /// Edit status for a order
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}")]
        public async Task<IActionResult> EditOrderStatus([FromBody] UpdataOrderStatus dto,[FromRoute] int id){
            if(!ModelState.IsValid){
                var state = ModelState
                .SelectMany(p=>p.Value.Errors)
                .Select(p=>p.ErrorMessage)
                .FirstOrDefault();
                return BadRequest(_messageServices.Message("error", state)); 
            }
            var result = await _orderServices.EditDataInDatabse(dto,id);
            return Ok(result); 
        }

}