using ecpmmerceApp.Application.Mapping;
using ecpmmerceApp.Application.Services;
using ecpmmerceApp.Application.Services.Authentication;
using ecpmmerceApp.Application.Services.AuthenticationService;
using ecpmmerceApp.Application.Services.Validation;
using ecpmmerceApp.Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.DependincyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationDependincies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();
            services.AddValidatorsFromAssemblyContaining<LoginUserValidation>();
            services.AddValidatorsFromAssemblyContaining<CreateProductValidation>();
            services.AddScoped<IValidateUser, ValidateUser>();
            services.AddScoped<IValidateProduct, ValidateProduct>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;

        }
    }
}
