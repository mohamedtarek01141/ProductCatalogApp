using AutoMapper;
using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.Category;
using ecpmmerceApp.Application.DTOs.Product;
using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Services
{
    public class CategoryService(ICategoryRepository _Category,IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> add(CategoryRequest category)
        {
            var MappedData = mapper.Map<Category>(category);
            var result = await _Category.AddAsync(MappedData);
            if (result > 0)
                return new ServiceResponse(true, "Category Added Successfully");
            else
                return new ServiceResponse(false, "Category Failed to Add");
        }

        public async Task<ServiceResponse> delete(Guid id)
        {
            var result = await _Category.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Category Deleted Successfully") :
                new ServiceResponse(false, "Category Failed To delete");
        }

        public async Task<IEnumerable<CategoryResponse>> getAll()
        {
            var categories = await _Category.GetAllAsync();
            if (!categories.Any())
                return [];
            return mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<CategoryResponse?> getByID(Guid id)
        {
            var category = await _Category.GetByIdAsync(id);
            if (category == null) return null;
            return mapper.Map<CategoryResponse>(category);
        }

        public async Task<ServiceResponse> update( Category category)
        {
            
            var result = await _Category.UpdateAsync(category);
            return result > 0 ? new ServiceResponse(true, "Category Updated")
                : new ServiceResponse(false, "Category failed to Update");
        }
    }
}
