namespace ECommerce.API.Interfaces.Order
{
    using ECommerce.API.Models.Order.CreateModels;
    
    public interface IOrderService
    {
        public bool IsValidOrder(CreateOrderModel createOrderModel);
    }
}
