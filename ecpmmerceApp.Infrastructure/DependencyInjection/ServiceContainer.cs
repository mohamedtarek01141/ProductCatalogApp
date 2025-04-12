using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Domain.Interface;
using ecpmmerceApp.Infrastructure.Data;
using ecpmmerceApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using EntityFramework.Exceptions.SqlServer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using ecpmmerceApp.Infrastructure.Middleware;
using ecpmmerceApp.Application.Services.Logging;
using ecpmmerceApp.Infrastructure.Services;
using ecpmmerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using ecpmmerceApp.Domain.Interface.Authentication;
using ecpmmerceApp.Infrastructure.Repositories.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ecpmmerceApp.Infrastructure.Interfaces;
using ProductCatalogApp.Infrastructure.Services;
using ProductCatalogApp.Domain.Interface.Cart;
using ProductCatalogApp.Infrastructure.Repositories.cart;
using ecommercApp.Application.Services.Cart;

namespace ecpmmerceApp.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureDependincies(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DEV")));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProductRepository, ProductRepository>();
           services.AddScoped(typeof(IAppLogger<>),typeof(SerilogLoggerAdapter<>));
            services.AddScoped<IPaymentMethod, PaymentmethodRepository>();
            services.AddScoped<IPaymentService, StripePaymentMethod>();
            services.AddScoped<Icart, CartRepository>();
            services.AddIdentity<AppUser, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication()
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
        options.LoginPath = "/Account/Login";
    });

            
            services.AddScoped<IUserManagment, UserManagment>();
            services.AddScoped<IRoleManagment, RoleManagment>();
            Stripe.StripeConfiguration.ApiKey = config["Stripe:SecretKey"];

            return services;
        }
        public static IApplicationBuilder UseInfrastructureServices(this IApplicationBuilder applicationBuilder)
        {

            applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>();
            return applicationBuilder;
        }

    }
}
