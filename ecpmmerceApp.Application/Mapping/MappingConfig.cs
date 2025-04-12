using AutoMapper;
using ecommercApp.Application.DTOs.cart;
using ecommercApp.Application.DTOs.Payment;
using ecpmmerceApp.Application.DTOs.Category;
using ecpmmerceApp.Application.DTOs.Product;
using ecpmmerceApp.Application.DTOs.User;
using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Mapping
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<Product,ProductResponse>();
            CreateMap<Product, ProductRequest>();
            CreateMap<ProductResponse, Product>();
            CreateMap<ProductResponse, ProductRequest>();
            CreateMap<ProductRequest, ProductResponse>();

            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>();

            CreateMap<LoginUser, AppUser>();
            CreateMap<CreateUser, AppUser>();
            CreateMap<PaymentMethod, PaymentMethodResponse>();
            CreateMap<CreateArchive, ArchivePayment>();
        }
    }
}
