using ecpmmerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecpmmerceApp.Domain.Interface
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<ICollection<Category>> GetAllAsync();
        Task<int> AddAsync(Category category);
        Task<int> UpdateAsync(Category category);
        Task<int> DeleteAsync(Guid id);

    }
}
