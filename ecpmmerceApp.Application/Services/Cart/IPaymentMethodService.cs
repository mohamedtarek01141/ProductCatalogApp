using ecommercApp.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommercApp.Application.Services.Cart
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethodResponse>> GetPaymentMethodsAsync();
    }
}
