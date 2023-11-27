using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Sales_Point.Core.Entities.Identity;
using Sales_Point.Core.Service;
using Sales_Point.Repository.Identity;
using Sales_Point.Service;
using System.Text;

namespace SalesPoint.APIs.Extensions
{
    public static class IdentityServicesExtensions
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration configuration)
        {

            Services.AddIdentity<AppUser, IdentityRole>(option => { })
                    .AddEntityFrameworkStores<AppIdentityDbContext>();




            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {


                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidAudience = configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });

            Services.AddScoped<ITokenService, TokenService>();

            return Services;
        }
    }
}
