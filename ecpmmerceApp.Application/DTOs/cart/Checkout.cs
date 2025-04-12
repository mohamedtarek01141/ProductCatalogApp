using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommercApp.Application.DTOs.cart
{
    public class Checkout
    {
        public Guid paymentMethod {  get; set; }
        public IEnumerable<ProcessCart> processCart { get; set; }
    }
}
