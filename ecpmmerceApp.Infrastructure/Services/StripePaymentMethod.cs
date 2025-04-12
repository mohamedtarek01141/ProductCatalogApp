using ecommercApp.Application.DTOs.cart;
using ecommercApp.Application.Services.Cart;
using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.Product;
using ecpmmerceApp.Domain.Entities;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Product = ecpmmerceApp.Domain.Entities.Product;

namespace ProductCatalogApp.Infrastructure.Services
{
    public class StripePaymentMethod : IPaymentService
    {
        public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts)
        {
            try
            {
                var lineItems = new List<SessionLineItemOptions>();
                foreach (var product in cartProducts)
                {
                    var productQuantity = carts.FirstOrDefault(p => p.productId == product.Id);
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = product.Name,
                            },
                            UnitAmount = (long)product.Price * 100,

                        },
                        Quantity = productQuantity!.Quantity
                    });

                }
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = ["usd"],
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "https://localhost:7036/payment-success",
                    CancelUrl = "https://localhost:7036/payment-success"
                };
                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                return new ServiceResponse { Success = true, Message = session.Url };
            }
            catch (Exception ex) 
            { 
                return new ServiceResponse { Success = false, Message = ex.Message };
            }
            }
            
        
    }
}
