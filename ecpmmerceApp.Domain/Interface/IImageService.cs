using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Infrastructure.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImage(IFormFile file);
        Task<bool> DeleteImage(string imageName);
    }
}
