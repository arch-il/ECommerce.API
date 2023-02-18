namespace ECommerce.API.Models.Order.UpdateModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class UpdateOrderModel
    {
        [Required]
        public int Id { get; set; }
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
