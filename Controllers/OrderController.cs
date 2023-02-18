namespace ECommerce.API.Controllers
{
    using ECommerce.API.Database.Context;
    using ECommerce.API.Database.Entites;
    using ECommerce.API.Interfaces.Order;
    using ECommerce.API.Models;
    using ECommerce.API.Models.Order.BuyPorductModels;
    using ECommerce.API.Models.Order.CreateModels;
    using ECommerce.API.Models.Order.UpdateModels;
    using ECommerce.API.Models.Order.ViewModels;
    using ECommerce.API.Models.Product.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ECommerceContext db;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, ECommerceContext db)
        {
            _logger = logger;
            _orderService = orderService;
            this.db = db;
        }

        [HttpGet("[action]")]
        public async Task<CustomResponseModel<IEnumerable<ViewOrderModel>>> GetAll()
        {
            try
            {
                var orders = await db.Order.Include(x => x.Product).ToListAsync();

                var viewOrderModels = new List<ViewOrderModel>();

                foreach (var order in orders)
                {
                    viewOrderModels.Add(new()
                    {
                        Id = order.Id,
                        CustomerId = order.CustomerId,
                        Product = new ViewProductModel()
                        {
                            Id = order.Product.Id,
                            Name = order.Product.Name,
                            Price = order.Product.Price,
                            Description = order.Product.Description
                        },
                        Quantity = order.Quantity,
                        TotalPrice = order.TotalPrice
                    });
                }

                return new CustomResponseModel<IEnumerable<ViewOrderModel>>()
                {
                    StatusCode = 200,
                    Result = viewOrderModels
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<IEnumerable<ViewOrderModel>>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<CustomResponseModel<ViewOrderModel>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return new CustomResponseModel<ViewOrderModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Please enter valid Id"
                    };

                var order = await db.Order.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);

                if (order == null)
                    return new CustomResponseModel<ViewOrderModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Order not found int Database"
                    };

                var viewOrderModel = new ViewOrderModel()
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    Product = new ViewProductModel()
                    {
                        Id = order.Product.Id,
                        Name = order.Product.Name,
                        Price = order.Product.Price,
                        Description = order.Product.Description
                    },
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice
                };

                return new CustomResponseModel<ViewOrderModel>()
                {
                    StatusCode = 200,
                    Result = viewOrderModel
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<ViewOrderModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }


        [HttpPost("[action]")]
        public async Task<CustomResponseModel<bool>> Create([FromQuery] CreateOrderModel createOrderModel)
        {
            try
            {
                if (!_orderService.IsValidOrder(createOrderModel))
                    return new CustomResponseModel<bool>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Enter valid data"
                    };

                var product = await db.Product.FirstOrDefaultAsync(x => x.Id == createOrderModel.ProductId);

                if (product == null)
                    return new CustomResponseModel<bool>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Product not found in Database"
                    };
                var order = new Order()
                {
                    CustomerId = createOrderModel.CustomerId,
                    Product = product,
                    Quantity = createOrderModel.Quantity,
                    TotalPrice = createOrderModel.TotalPrice
                };

                await db.Order.AddAsync(order);
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
        public async Task<CustomResponseModel<ViewOrderModel>> Update([FromQuery] UpdateOrderModel updateOrderModel)
        {
            try
            {
                var order = await db.Order.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == updateOrderModel.Id);
                var product = await db.Product.FirstOrDefaultAsync(x => x.Id == updateOrderModel.ProductId);

                if (order == null || product == null)
                    return new CustomResponseModel<ViewOrderModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Order or Product not found in Database"
                    };

                

                var resultOrder = new Order()
                {
                    CustomerId = updateOrderModel.CustomerId,
                    Product = product,
                    Quantity = updateOrderModel.Quantity,
                    TotalPrice = updateOrderModel.TotalPrice
                };

                db.Update(resultOrder);
                await db.SaveChangesAsync();

                var viewOrderModel = new ViewOrderModel()
                {
                    Id = updateOrderModel.Id,
                    CustomerId = updateOrderModel.CustomerId,
                    Product = new ViewProductModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description
                    },
                    Quantity = updateOrderModel.Quantity,
                    TotalPrice = updateOrderModel.TotalPrice
                };

                return new CustomResponseModel<ViewOrderModel>()
                {
                    StatusCode = 200,
                    Result = viewOrderModel
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<ViewOrderModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<CustomResponseModel<ViewOrderModel>> Delete(int id)
        {
            try
            {
                if (id < 0)
                    return new CustomResponseModel<ViewOrderModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Please enter valid Id"
                    };

                var order = await db.Order.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);

                if (order == null)
                    return new CustomResponseModel<ViewOrderModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Order not found int Database"
                    };

                db.Order.Remove(order);
                await db.SaveChangesAsync();

                var viewOrderModel = new ViewOrderModel()
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    Product = new ViewProductModel()
                    {
                        Id = order.Product.Id,
                        Name = order.Product.Name,
                        Price = order.Product.Price,
                        Description = order.Product.Description
                    },
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice
                };

                return new CustomResponseModel<ViewOrderModel>()
                {
                    StatusCode = 200,
                    Result = viewOrderModel
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<ViewOrderModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        [HttpPut("[action]")]
        public async Task<CustomResponseModel<bool>> BuyProduct([FromQuery] BuyProductModel buyProductModel)
        {
            try
            {
                if (buyProductModel.ProductId < 0 ||
                    buyProductModel.CustomerId < 0 ||
                    buyProductModel.Quantity <= 0)
                    return new CustomResponseModel<bool>()
                    {
                        StatusCode = 400,
                        Result = false,
                        ErrorMessage = "Invalid input data"
                    };

                var product = await db.Product.FirstOrDefaultAsync(x => x.Id == buyProductModel.ProductId);

                if (product == null)
                    return new CustomResponseModel<bool>()
                    {
                        StatusCode = 400,
                        Result = false,
                        ErrorMessage = "Product not found in Database"
                    };

                var order = new Order()
                {
                    CustomerId = buyProductModel.CustomerId,
                    Product = product,
                    Quantity = buyProductModel.Quantity,
                    TotalPrice = buyProductModel.Quantity * product.Price
                };

                await db.Order.AddAsync(order);
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
    }
}
