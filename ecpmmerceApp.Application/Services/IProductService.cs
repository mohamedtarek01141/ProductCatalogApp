using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.Product;
using ecpmmerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductResponse>> getAll();
        public Task<ProductResponse?> getByID(Guid id);
        public Task<ServiceResponse> add(ProductRequest produect);
        public Task<ServiceResponse> update(ProductResponse product);
        public Task<ServiceResponse> delete(Guid id);
        public Task<IEnumerable<ProductResponse>> getActiveProducts();

    }
}
