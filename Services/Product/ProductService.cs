namespace ECommerce.API.Services.Product
{
    using ECommerce.API.Interfaces.Product;
    using ECommerce.API.Models.Product.CreateModels;

    public class ProductService : IProductService
    {
        public bool IsValidProduct(CreateProductModel createProductModel)
        {
            return !(createProductModel == null ||
                     createProductModel.Name.Length < 3 ||
                     createProductModel.Description.Length < 3);
        }
    }
}
