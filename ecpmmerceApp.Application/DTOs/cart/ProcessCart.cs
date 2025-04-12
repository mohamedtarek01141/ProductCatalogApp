using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommercApp.Application.DTOs.cart
{
    public class ProcessCart
    {
        required
        public Guid productId {  get; set; }
        public int Quantity { get; set; }
    }
}
