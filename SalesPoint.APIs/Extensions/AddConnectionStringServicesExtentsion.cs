using Microsoft.EntityFrameworkCore;
using Sales_Point.Repository.Data;
using Sales_Point.Repository.Identity;
using StackExchange.Redis;

namespace SalesPoint.APIs.Extensions
{
    public static class AddConnectionStringServicesExtentsion
    {
        public static IServiceCollection AddConnectionStringServices(this IServiceCollection Services, IConfiguration configuration)
        {

            Services.AddDbContext<StoreContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var connection = configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });


            Services.AddDbContext<AppIdentityDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });







            return Services;
        }

    }
}
