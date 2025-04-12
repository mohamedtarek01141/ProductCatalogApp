using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommercApp.Application.DTOs.cart
{
    public class CreateArchive
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ProductId { get; set; } 
        [Required]  
        public int Quantity { get; set; }
    }
}
