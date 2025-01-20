using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.Category;
using ecpmmerceApp.Application.DTOs.Product;
using ecpmmerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryResponse>> getAll();
        public Task<CategoryResponse?> getByID(Guid id);
        public Task<ServiceResponse> add(CategoryRequest category);
        public Task<ServiceResponse> update( Category category);
        public Task<ServiceResponse> delete(Guid id);
    }
}
