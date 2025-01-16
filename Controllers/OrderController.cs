using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("/order")]
public class OrderController:ControllerBase{
    private readonly OrderServices orderServices;
    public OrderController(OrderServices _orderServices){
        orderServices = _orderServices;
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
[HttpPost("{id}")]
public async Task<IActionResult> EditOrderStatus([FromBody] UpdataOrderStatus dto,[FromRoute] int id){
    if(!ModelState.IsValid){
        var state = ModelState
        .SelectMany(p=>p.Value.Errors)
        .Select(p=>p.ErrorMessage)
        .FirstOrDefault();
        return BadRequest(state); 
    }
    var result = await orderServices.EditDataInDatabse(dto,id);
    return Ok(result); 
}

}