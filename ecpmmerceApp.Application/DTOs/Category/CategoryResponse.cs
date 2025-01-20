using ecpmmerceApp.Application.DTOs.Product;
using ecpmmerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.DTOs.Category
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<ProductResponse>? Products { get; set; }

    }
}
