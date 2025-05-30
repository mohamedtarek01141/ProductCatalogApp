﻿using ecpmmerceApp.Application.DTOs.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.DTOs.Product
{
    public class ProductResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
        public IFormFile? ImagePath { get; set; }
        public string Image { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }

        public CategoryResponse? Category { get; set; }
        public Guid Categoryid { get; set; }

    }
}
