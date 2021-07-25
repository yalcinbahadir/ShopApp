using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAppTekrar.Identity
{
    public class AppIdContext:IdentityDbContext<User>
    {
        public AppIdContext(DbContextOptions<AppIdContext> options) : base(options)
        {

        }
    }
}
