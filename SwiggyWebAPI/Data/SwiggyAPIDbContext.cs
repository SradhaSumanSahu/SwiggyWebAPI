using Microsoft.EntityFrameworkCore;
using SwiggyWebAPI.Models.Customer;
using SwiggyWebAPI.Models.Order;
using SwiggyWebAPI.Models.Product;

namespace SwiggyWebAPI.Data
{
    public class SwiggyAPIDbContext : DbContext
    {
        public SwiggyAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CustomerModel> CustomerModel { get; set; }
        public DbSet<OrderModel> OrderModel { get; set; }
        public DbSet<ProductModel> ProductModel { get; set; }
    }
}
