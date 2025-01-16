using System.ComponentModel.DataAnnotations;

public class UpdataOrderStatus{
    [Required(ErrorMessage = "Status is required")]
    public OrderStatus status{get;set;}
}