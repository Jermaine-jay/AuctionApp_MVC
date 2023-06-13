using AunctionApp.BLL.Implementations;
using AunctionApp.BLL.Interfaces;
using AunctionApp.DAL.Database;
using AunctionApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using TodoList.DAL.Repository;

namespace AunctionAppMVC.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<AunctionAppDbContext>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddHttpContextAccessor();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AunctionAppDbContext>()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });
            services.AddHttpContextAccessor();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/SignIn";
            });
        }

        public static void Configure(IServiceProvider serviceProvider)
        {
            // Other app configurations

            // Create roles
            CreateRoles(serviceProvider).Wait();

            // Other app configurations
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Check if the roles exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                // Create Admin role
                var role = new IdentityRole("Admin");
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                // Create User role
                var role = new IdentityRole("User");
                await roleManager.CreateAsync(role);
            }
        }
    }
}
