using ecpmmerceApp.Infrastructure.Data;
using ecpmmerceApp.Domain.Entities;
using ProductCatalogApp.Domain.Interface.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogApp.Infrastructure.Repositories.cart
{
    public class CartRepository(ApplicationDbContext context) : Icart
    {
        public async Task<int> SaveCheckoutHistory(IEnumerable<ArchivePayment> checkouts)
        {
            context.ArchivePayments.AddRange(checkouts);
            return await context.SaveChangesAsync();  
        }
    }
}
