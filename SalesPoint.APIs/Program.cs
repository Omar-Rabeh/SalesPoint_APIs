using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Sales_Point.Core.Entities.Identity;
using Sales_Point.Repository.Data;
using Sales_Point.Repository.Identity;
using SalesPoint.APIs.Extensions;
using SalesPoint.APIs.Middleware;

namespace SalesPoint.APIs
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddConnectionStringServices(builder.Configuration);


            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServices(builder.Configuration);

            #endregion


            var app = builder.Build();


            #region Update database and  Data Seeding


            using var Scope = app.Services.CreateScope(); 
            var Services = Scope.ServiceProvider; 

            var lggerfactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = Services.GetRequiredService<StoreContext>(); 

                await dbContext.Database.MigrateAsync();

                var IdentityContext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityContext.Database.MigrateAsync();


                await StoreContextSeed.SeedAsync(dbContext);


                var userManager = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
            }
            catch (Exception e)
            {
                var loger = lggerfactory.CreateLogger<Program>();
                loger.LogError(e, "An Error Occurred  During Apply Database");
            }

            #endregion



            #region Configure the HTTP request pipeline

            app.UseMiddleware<ExceptionMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();

                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");


            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();



            app.UseStaticFiles();

            app.MapControllers();

            app.Run();

            #endregion
        }
    }
}