using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales_Point.Core.Entities.Identity;

namespace Sales_Point.Repository.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {

        public AppIdentityDbContext(DbContextOptions options): base(options)
        {
            
        }
    }
}
