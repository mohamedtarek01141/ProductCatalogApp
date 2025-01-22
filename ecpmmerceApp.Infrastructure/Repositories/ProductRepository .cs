using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Domain.Interface;
using ecpmmerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ecpmmerceApp.Infrastructure.Repositories
{
    internal class ProductRepository(ApplicationDbContext context) : IProductRepository
    {
        public async Task<int> AddAsync(Product product)
        {
            context.Set<Product>().Add(product);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var Current = await GetByIdAsync(id);
            if (Current == null)
            {
                return 0;
            }
            context.Set<Product>().Remove(Current);
            return await context.SaveChangesAsync();
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await context.Set<Product>().Include(p => p.Category).AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await context.FindAsync<Product>(id);
        }

        public async Task<int> UpdateAsync(Product product)
        {
            context.Set<Product>().Update(product);
            return await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await context.Set<Product>().Where(product => 
                                                 product.StartDate.AddDays((double)product.Duration!) >= DateTime.Now &&
                                                 product.StartDate<=DateTime.Now)
                               .OrderBy(product => product.StartDate).Include(p => p.Category).ToListAsync();
        }
    }
}
