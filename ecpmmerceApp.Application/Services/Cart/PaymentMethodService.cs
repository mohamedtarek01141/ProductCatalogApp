using AutoMapper;
using ecommercApp.Application.DTOs.Payment;
using ecpmmerceApp.Domain.Entities;
using ProductCatalogApp.Domain.Interface.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommercApp.Application.Services.Cart
{
    public class PaymentMethodService(IPaymentMethod paymentMethod,IMapper mapper) : IPaymentMethodService
    {
        public async Task<IEnumerable<PaymentMethodResponse>> GetPaymentMethodsAsync()
        {
            var PaymentMethods=await paymentMethod.GetPaymentMethods();
            if (!PaymentMethods.Any())
                return [];
            else
                return  mapper.Map<IEnumerable<PaymentMethodResponse>>(PaymentMethods);
        }
    }
}
