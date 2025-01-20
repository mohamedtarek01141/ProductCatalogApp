using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Infrastructure.Data;
using ecpmmerceApp.Application.Services;
using ecpmmerceApp.Application.DTOs.Product;
using Microsoft.AspNetCore.Identity;
using ecpmmerceApp.Domain.Interface.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using ecpmmerceApp.Application.Services.Logging;

namespace ProductCatalogApp.Controllers
{
    [Authorize (Roles ="Admin")]

    public class ProductController(IProductService productService,IWebHostEnvironment webHostEnvironment,IMapper mapper
        ,IAppLogger<ProductController> appLogger,ICategoryService categoryService )  : Controller
    {

        public async Task<IActionResult> Index()
        {
            var Products =await productService.getAll();

            return View(Products);
        }
        

        public async Task<IActionResult> Details(Guid id)
        {
            var product =await productService.getByID(id);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.getAll();
      

            var model = new ProductRequest
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(model);
        }

        
        [HttpPost]
        
        public async Task<IActionResult> Create(ProductRequest product)
        {
            if (User.Identity!.IsAuthenticated) 
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                    product.CreatedByUserId = userId!;
                else
                    ModelState.AddModelError("", "Invalid User");
            }        
            var categories = await categoryService.getAll();
            product.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            if (ModelState.IsValid)
            {
                var extension = Path.GetExtension(product.ImagePath.FileName);
                // Process the uploaded image
                if (product.ImagePath != null)
                {
                    var imagename =$"{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine($"{webHostEnvironment.WebRootPath }/images/product", imagename);

                    // Save the file to the server
                    using var stream = System.IO.File.Create(filePath);
                    await product.ImagePath.CopyToAsync(stream);
                    product.Image = imagename;
                }

                product.CreationDate=DateTime.Now;
                var res = await productService.add(product);
                if (res.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var err in res.Errors)
                    {
                        ModelState.AddModelError("", err);
                    }
                
                }
               
            }
           
            return View(product);
        }

        // GET: Product/Edit
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await productService.getByID(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductResponse product)
        {
            if (!ModelState.IsValid)
            {
                return View(product); // Return the same view to display validation errors
            }

            if (product.ImagePath != null)
            {
                // Get file extension
                var extension = Path.GetExtension(product.ImagePath.FileName);

                // Generate a unique image name
                var imageName = $"{Guid.NewGuid()}{extension}";

                // Define the file path to save the image
                var filePath = Path.Combine($"{webHostEnvironment.WebRootPath}/images/product", imageName);

                // Save the uploaded file
                using var stream = System.IO.File.Create(filePath);
                await product.ImagePath.CopyToAsync(stream);

                // Update the image path in the product object
                product.Image = imageName;
            }

            // Update the product
            var res = await productService.update(product);

            if (res.Success)
            {
                // Log the action
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                appLogger.LogInformation($"User with UserId: {userId} updated product: {product.Name}");

                return RedirectToAction(nameof(Index));
            }

            // Add errors to the model state if the update fails
            foreach (var err in res.Errors)
            {
                ModelState.AddModelError(string.Empty, err);
            }

            return View(product); // Return view with validation messages
        }


        

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await productService.delete(id);
         
            return RedirectToAction("Index");
        }


    }
}
