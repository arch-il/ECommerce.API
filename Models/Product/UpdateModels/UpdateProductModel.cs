namespace ECommerce.API.Models.Product.UpdateModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class UpdateProductModel
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
