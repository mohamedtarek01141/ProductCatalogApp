using ecpmmerceApp.Application.DTOs.Product;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Validation
{
    public class CreateProductValidation:AbstractValidator<ProductRequest>
    {
        public CreateProductValidation()
        {
            RuleFor(p => p.Name)
                        .NotEmpty().WithMessage("Product name is required.")
                        .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.");



            RuleFor(p => p.StartDate)
                .GreaterThanOrEqualTo(DateTime.Now.AddDays(-1)).WithMessage("Start date must be in the future.");

            RuleFor(p => p.Duration)
                .GreaterThan(0).WithMessage("Duration must be greater than zero.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        

        RuleFor(p => p.Categoryid)
                .NotEmpty()
                .WithMessage("Category ID is required.");
            When(p => string.IsNullOrEmpty(p.Image), () =>
            {
                RuleFor(p => p.ImagePath)
                    .NotNull().WithMessage("ImagePath is required.")
                    .Must(IsValidImageFile).WithMessage("ImagePath must be a JPG, JPEG, or PNG file.")
                    .Must(IsValidImageSize).WithMessage("ImagePath size must not exceed 1MB.");
            });
        }

        private bool IsValidImageFile(IFormFile? imageFile)
        {
            if (imageFile == null)
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(imageFile.FileName)?.ToLower();

            return allowedExtensions.Contains(fileExtension);
        }

        // Validate the image size (1MB limit)
        private bool IsValidImageSize(IFormFile? imageFile)
        {
            if (imageFile == null)
                return false;

            const long MaxSizeInBytes = 1 * 1024 * 1024; // 1MB in bytes

            return imageFile.Length <= MaxSizeInBytes;
        }
    }
}
