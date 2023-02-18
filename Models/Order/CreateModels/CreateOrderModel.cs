namespace ECommerce.API.Models.Order.CreateModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class CreateOrderModel
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
