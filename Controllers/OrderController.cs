using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("/order")]
public class OrderController:ControllerBase{
    private readonly OrderServices orderServices;
    private readonly MessageServices _messageServices;
    public OrderController(OrderServices _orderServices,MessageServices messageServices){
        orderServices = _orderServices;
        _messageServices = messageServices;
    }

[HttpPost]
public async Task<IActionResult> NewOrder([FromBody] Cart cart){
        var result = await orderServices.AddOrder(cart);
        return Ok(result);
}
[HttpGet("{id}")]
public async Task<IActionResult> GetOrders([FromRoute] int id){
    var result = await orderServices.GetAllOrdersById(id);
    return Ok(result);
}
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
    var result = await orderServices.EditDataInDatabse(dto,id);
    return Ok(result); 
}

}