using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Sales_Point.Core.Entities;
using Sales_Point.Core.Entities.OrderAggregate;
using Sales_Point.Repository.Data.Configurations;

namespace Sales_Point.Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> Option) : base(Option) { }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



        
        //modelBuilder.ApplyConfiguration(new ProductConfig());


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
