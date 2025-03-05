using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/order")]
public class OrderController:ControllerBase{
    private readonly IOrderServices _orderServices;
    public OrderController(IOrderServices orderServices){
        _orderServices = orderServices;
    }

    /// <summary>
    /// Add new order, only if payment will be succesfull
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] Cart cart){
            var result = await _orderServices.AddOrder(cart);
            if(!result.IsSuccess){
                return BadRequest(result);
            }
            return Created();
    }

    /// <summary>
    /// Return one specific order
    /// </summary>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder([FromRoute] int id){
        var result = await _orderServices.GetOrder(id);
        if(!result.IsSuccess){
            return NotFound(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Return all user orders
    /// </summary>
    [Authorize]
    [HttpGet("/orders")]
    public async Task<IActionResult> GetOrders(){
        var result = await _orderServices.GetOrders();
        if(!result.IsSuccess){
            return NotFound(result);
        }
        return Ok(result);
    }
    
    /// <summary>
    /// Edit status for a order
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("{id}")]
    public async Task<IActionResult> EditOrderStatus([FromBody] UpdataOrderStatus dto,[FromRoute] int id){
        var result = await _orderServices.EditDataInDatabse(dto,id);
        if(!result.IsSuccess){
            return NotFound(result);
        }
        return Ok(result);
    }

}