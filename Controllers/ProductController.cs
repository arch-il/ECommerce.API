namespace ECommerce.API.Controllers
{
    using ECommerce.API.Database.Context;
    using ECommerce.API.Database.Entites;
    using ECommerce.API.Interfaces.Product;
    using ECommerce.API.Models;
    using ECommerce.API.Models.Product.CreateModels;
    using ECommerce.API.Models.Product.UpdateModels;
    using ECommerce.API.Models.Product.ViewModels;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ECommerceContext db;

        public ProductController(ILogger<ProductController> logger, IProductService productService, ECommerceContext db)
        {
            _logger = logger;
            _productService = productService;
            this.db = db;
        }

        [HttpGet("[action]")]
        public async Task<CustomResponseModel<IEnumerable<ViewProductModel>>> GetAll()
        {
            try
            {
                var products = await db.Product.ToListAsync();

                var viewProductModels = new List<ViewProductModel>();

                foreach (var product in products)
                {
                    viewProductModels.Add(new()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description
                    });
                }

                return new CustomResponseModel<IEnumerable<ViewProductModel>>()
                {
                    StatusCode = 200,
                    Result = viewProductModels
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<IEnumerable<ViewProductModel>>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<CustomResponseModel<ViewProductModel>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return new CustomResponseModel<ViewProductModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Please enter valid Id"
                    };

                var product = await db.Product.FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return new CustomResponseModel<ViewProductModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Product not found int Database"
                    };

                var viewProductModel = new ViewProductModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                };

                return new CustomResponseModel<ViewProductModel>()
                {
                    StatusCode = 200,
                    Result = viewProductModel
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<ViewProductModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }


        [HttpPost("[action]")]
        public async Task<CustomResponseModel<bool>> Create([FromQuery] CreateProductModel createProductModel)
        {
            try
            {
                if (!_productService.IsValidProduct(createProductModel))
                    return new CustomResponseModel<bool>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Enter valid data"
                    };

                var product = new Product()
                {
                    Name = createProductModel.Name,
                    Price = createProductModel.Price,
                    Description = createProductModel.Description
                };

                await db.Product.AddAsync(product);
                await db.SaveChangesAsync();

                return new CustomResponseModel<bool>()
                {
                    StatusCode = 200,
                    Result = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<bool>()
                {
                    StatusCode = 400,
                    Result = false,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        [HttpPut("[action]")]
        public async Task<CustomResponseModel<ViewProductModel>> Update([FromQuery] UpdateProductModel updateProductModel)
        {
            try
            {
                var product = await db.Product.FirstOrDefaultAsync(x => x.Id == updateProductModel.Id);

                if (product == null)
                    return new CustomResponseModel<ViewProductModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Product not found in Database"
                    };

                var resultProduct = new Product()
                {
                    Id = updateProductModel.Id,
                    Name = updateProductModel.Name,
                    Price = updateProductModel.Price,
                    Description = updateProductModel.Description
                };

                db.Update(resultProduct);
                await db.SaveChangesAsync();

                var viewProductModel = new ViewProductModel()
                {
                    Id = updateProductModel.Id,
                    Name = updateProductModel.Name,
                    Price = updateProductModel.Price,
                    Description = updateProductModel.Description
                };

                return new CustomResponseModel<ViewProductModel>()
                {
                    StatusCode = 200,
                    Result = viewProductModel
                };
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<ViewProductModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<CustomResponseModel<ViewProductModel>> Delete(int id)
        {
            try
            {
                if (id < 0)
                    return new CustomResponseModel<ViewProductModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Please enter valid Id"
                    };

                var product = await db.Product.FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return new CustomResponseModel<ViewProductModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Product not found int Database"
                    };

                db.Product.Remove(product);
                await db.SaveChangesAsync();

                var viewProductModel = new ViewProductModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                };

                return new CustomResponseModel<ViewProductModel>()
                {
                    StatusCode = 200,
                    Result = viewProductModel
                };
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<ViewProductModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }
    }
}
