namespace ECommerce.API.Services.Order
{
    using ECommerce.API.Interfaces.Order;
    using ECommerce.API.Models.Order.CreateModels;

    public class OrderService : IOrderService
    {
        public bool IsValidOrder(CreateOrderModel createOrderModel)
        {
            return !(createOrderModel == null ||
                     createOrderModel.TotalPrice < 0 ||
                     createOrderModel.ProductId < 0 ||
                     createOrderModel.CustomerId < 0);
        }
    }
}
