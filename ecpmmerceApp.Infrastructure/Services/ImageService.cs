using ecpmmerceApp.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecpmmerceApp.Infrastructure.Interfaces;

namespace ProductCatalogApp.Infrastructure.Services
{
    public class ImageService(IWebHostEnvironment webHostEnvironment) : IImageService
    {


        public async Task<string> SaveImage(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (file != null)
            {
                var imagename = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine($"{webHostEnvironment.WebRootPath}/images/product", imagename);
                using var stream = System.IO.File.Create(filePath);
                await file.CopyToAsync(stream);
               return imagename;
            }
            return null!;
        }
        public async Task<bool> DeleteImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName)) return false;

            var filePath = Path.Combine($"{webHostEnvironment.WebRootPath}/images/product", imageName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }

    }
}
