namespace ECommerce.API.Models.Order.ViewModels
{
    using ECommerce.API.Models.Product.ViewModels;
    using System.ComponentModel.DataAnnotations;
    
    public class ViewOrderModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public ViewProductModel Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
