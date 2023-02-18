namespace ECommerce.API.Models.Product.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class ViewProductModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
