using AutoMapper;
using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.Product;
using ecpmmerceApp.Application.Services.Validation;
using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Domain.Interface;
using ecpmmerceApp.Infrastructure.Interfaces;
using FluentValidation;
using ecpmmerceApp.Infrastructure.Interfaces;

namespace ecpmmerceApp.Application.Services
{
    public class ProductService(IProductRepository _Product, IMapper mapper, IValidator<ProductRequest> createproductvalidation
        , IValidateProduct validateProduct, IImageService imageService) : IProductService
    {

        public async Task<ServiceResponse> add(ProductRequest product)
        {
            var validateRes = await validateProduct.ValidateProductAsync(product, createproductvalidation);
            if (!validateRes.Success)
                return new ServiceResponse { Errors = validateRes.Errors };
            product.Image = await imageService.SaveImage(product.ImagePath!);
            var MappedData = mapper.Map<Product>(product);
            var result = await _Product.AddAsync(MappedData);
            if (result > 0)
                return new ServiceResponse(true, "Product Added Successfully");
            else
                return new ServiceResponse(false, "Product Failed to Add", new List<string> { "Product Failed to Add" });

        }

        public async Task<ServiceResponse> delete(Guid id)
        {
            var product = await _Product.GetByIdAsync(id);
            if (product == null)
                return new ServiceResponse(false, "Product Not Found");
            await imageService.DeleteImage(product.Image!);
            var result = await _Product.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Product Deleted Successfully") :
                new ServiceResponse(false, "Product Failed To delete");
        }

        public async Task<IEnumerable<ProductResponse>> getAll()
        {
            var products = await _Product.GetAllAsync();
            if (!products.Any())
                return [];
            return mapper.Map<IEnumerable<ProductResponse>>(products);
        }
        public async Task<IEnumerable<ProductResponse>> getActiveProducts()
        {
            var products = await _Product.GetActiveProductsAsync();
            if (!products.Any())
                return [];
            return mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse?> getByID(Guid id)
        {
            var product = await _Product.GetByIdAsync(id);
            if (product == null) return null;
            return mapper.Map<ProductResponse>(product);
        }

        public async Task<ServiceResponse> update(ProductResponse product)
        {
            var ep = await getByID(product.Id);
            if (ep == null)
                return new ServiceResponse(false, "Product Not Found");
            var MappedData = mapper.Map<ProductRequest>(product);

            var validateRes = await validateProduct.ValidateProductAsync(MappedData, createproductvalidation);
            if (!validateRes.Success)
                return new ServiceResponse { Errors = validateRes.Errors };
            if (MappedData.ImagePath != null)
            {
                await imageService.DeleteImage(ep.Image!);

                product.Image = await imageService.SaveImage(MappedData.ImagePath);
            }

            var MappedData2 = mapper.Map<Product>(product);

            var result = await _Product.UpdateAsync(MappedData2);
            return result > 0 ? new ServiceResponse(true, "Product Updated")
                : new ServiceResponse(false, "Product failed to Update", new List<string> { "Product failed to Update" });
        }
    }
}
