using ecpmmerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogApp.Domain.Interface.Cart
{
    public interface Icart
    {
        Task<int> SaveCheckoutHistory(IEnumerable<ArchivePayment> archives);
    }
}
