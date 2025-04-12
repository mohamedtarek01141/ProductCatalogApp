using ecommercApp.Application.DTOs.cart;
using ecpmmerceApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommercApp.Application.Services.Cart.Archive
{
    public interface ICartService
    {
        Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateArchive> archives);
        Task<ServiceResponse> Checkout(Checkout checkout);

    }
}
