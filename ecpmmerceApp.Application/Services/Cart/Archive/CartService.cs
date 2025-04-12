using AutoMapper;
using ecommercApp.Application.DTOs.cart;
using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.Product;
using ecpmmerceApp.Application.Services;
using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Domain.Interface;
using ecpmmerceApp.Domain.Entities;
using ProductCatalogApp.Domain.Interface.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommercApp.Application.Services.Cart.Archive
{
    public class CartService(Icart cartInterface,IPaymentService paymentService, IMapper mapper,
        IProductRepository productRepository,IPaymentMethodService paymentMethodService) : ICartService
    {
        public async Task<ServiceResponse> Checkout(Checkout checkout)
        {
            var (totalAmount, products) = await GetTotalAmountWithProductsAsync(checkout.processCart);
            var paymentMethods =await paymentMethodService.GetPaymentMethodsAsync();
            if (checkout.paymentMethod == paymentMethods.FirstOrDefault()!.Id)
                return await paymentService.Pay(totalAmount, products, checkout.processCart);
            return new ServiceResponse(false, "Failed checkout");
        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateArchive> archives)
        {
            var mappedData = mapper.Map<IEnumerable<ArchivePayment>>(archives);
            var result =await cartInterface.SaveCheckoutHistory(mappedData);
            return result > 0 ? new ServiceResponse(Success: true, Message: "Archived Saved") :
                new ServiceResponse(Success: false, Message: "Archive Failed to save");

        }
        public async Task<(decimal TotalAmount, IEnumerable<Product> Products)> GetTotalAmountWithProductsAsync(IEnumerable<ProcessCart> carts)
        {
            if (carts == null || !carts.Any())
                return (0, Enumerable.Empty<Product>());

            // Retrieve all products
            var products = await productRepository.GetAllAsync();
            if (!products.Any()) return (0, []);
             
            // Calculate total amount
            decimal totalAmount = carts.Sum(c =>
                products.FirstOrDefault(p => p.Id == c.productId)?.Price * c.Quantity ?? 0
            );

            // Filter the products that exist in the cart
            var cartProducts = products
                .Where(p => carts.Any(c => c.productId == p.Id))
                .ToList();

            return (totalAmount,mapper.Map<IEnumerable<Product>>(products));
        }


    }
}
