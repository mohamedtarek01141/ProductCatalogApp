using ecommercApp.Application.DTOs.cart;
using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Domain.Entities;

namespace ecommercApp.Application.Services.Cart
{
    public interface IPaymentService
    {
        Task<ServiceResponse> Pay(decimal totalAmount,IEnumerable<Product> cartProducts,IEnumerable<ProcessCart> carts);
    }
}
