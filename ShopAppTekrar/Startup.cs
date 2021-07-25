using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApp.BussinessLayer.Abstract;
using ShopApp.BussinessLayer.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete;
using ShopAppTekrar.Identity;

namespace ShopAppTekrar
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Domein classes dependency injections
            services.AddScoped<IProductRepository, EFCOREProductRepository>();
            services.AddScoped<ICategoryRepository, EFCORECategoryRepository>();
            services.AddScoped<IProductServices, ProductManager>();
            services.AddScoped<ICategoryServices, CategoryManager>();

            //User
            services.AddDbContext<AppIdContext>(options=>options.
            UseSqlServer(@"Server=.\SQLEXPRESS02; Initial catalog=FullOverview; MultipleActiveResultSets = true; Integrated Security=true"));
         
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppIdContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>{
                //Pasword
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                //Lockout
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;
                //User
                options.User.RequireUniqueEmail = true;
                //SignIn
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            });

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder()
                {
                    HttpOnly = true,
                    Name="ShopAppSecurity.Cookie"                
                };
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed();
            }
            //For wwwroot (images, css, js, etc files/folder)
            app.UseStaticFiles();
            app.UseRouting();
            //User
            app.UseAuthentication();
            //Authorization
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                         name:"default",
                         pattern: "{controller=Home}/{action=Index}/{id?}"

                    );
            });
        }
    }
}
