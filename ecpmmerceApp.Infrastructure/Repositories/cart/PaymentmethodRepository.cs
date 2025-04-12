using ecpmmerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ecpmmerceApp.Domain.Entities;
using ProductCatalogApp.Domain.Interface.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogApp.Infrastructure.Repositories.cart
{
    public class PaymentmethodRepository(ApplicationDbContext context) : IPaymentMethod
    {
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
        {
            return await context.PaymentMethods.AsNoTracking().ToListAsync();  
        }
    }
}
