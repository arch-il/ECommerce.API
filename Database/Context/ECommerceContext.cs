namespace ECommerce.API.Database.Context
{
    using Microsoft.EntityFrameworkCore;
    using ECommerce.API.Database.Entites;

    public class ECommerceContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }

        public ECommerceContext(DbContextOptions<ECommerceContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
