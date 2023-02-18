namespace ECommerce.API.Models.Product.CreateModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class CreateProductModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
