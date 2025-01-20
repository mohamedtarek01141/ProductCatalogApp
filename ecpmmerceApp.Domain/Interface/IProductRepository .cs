using ecpmmerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecpmmerceApp.Domain.Interface
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id); 
        Task<ICollection<Product>> GetAllAsync(); 
        Task<int> AddAsync(Product product); 
        Task<int> UpdateAsync(Product product); 
        Task<int> DeleteAsync(Guid id);
        Task<IEnumerable<Product>> GetActiveProductsAsync();

    }
}
