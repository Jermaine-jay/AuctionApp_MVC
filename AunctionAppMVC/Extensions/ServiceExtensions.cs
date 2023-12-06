using AunctionApp.BLL.Extensions;
using AunctionApp.BLL.Implementations;
using AunctionApp.BLL.Interfaces;
using AunctionApp.DAL.Database;
using AunctionApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using AunctionApp.DAL.Repository;

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
            services.AddScoped<IGenerateEmailVerificationPage, GenerateEmailVerificationPage>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRecoveryService, RecoveryService>();
			services.AddScoped<IServiceFactory, ServiceFactory>();
			services.AddHttpContextAccessor();
			services.Configure<DataProtectionTokenProviderOptions>(x => x.TokenLifespan = TimeSpan.FromMinutes(10));

		}


        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AunctionAppDbContext>()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddPasswordlessLoginTotpTokenProvider();


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
    }
}
