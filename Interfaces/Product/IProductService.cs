namespace ECommerce.API.Interfaces.Product
{
    using ECommerce.API.Models.Product.CreateModels;
    
    public interface IProductService
    {
        public bool IsValidProduct(CreateProductModel createProductModel);
    }
}
