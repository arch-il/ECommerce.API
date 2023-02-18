namespace ECommerce.API.Database.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
