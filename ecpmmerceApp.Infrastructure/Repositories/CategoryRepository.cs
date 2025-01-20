using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Domain.Interface;
using ecpmmerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Infrastructure.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
    {
        public async Task<int> AddAsync(Category category)
        {
            context.Set<Category>().Add(category);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var Current = await GetByIdAsync(id);
            if (Current == null)
            {
                return 0;
            }
            context.Set<Category>().Remove(Current);
            return await context.SaveChangesAsync();
        }

        public async Task<ICollection<Category>> GetAllAsync()
        {
            return await context.Set<Category>().AsNoTracking().ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await context.FindAsync<Category>(id);
        }

       

        public async Task<int> UpdateAsync(Category category)
        {
            context.Set<Category>().Update(category);
            return await context.SaveChangesAsync();
        }
        
    }
}
