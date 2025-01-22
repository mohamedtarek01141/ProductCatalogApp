using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ecpmmerceApp.Application.Services;
using ecpmmerceApp.Application.DTOs.Product;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ecpmmerceApp.Application.Services.Logging;
using ecpmmerceApp.Domain.Entities;

namespace ProductCatalogApp.Controllers
{
    [Authorize (Roles ="Admin")]

    public class ProductController(IProductService productService,IWebHostEnvironment webHostEnvironment
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
            if(product!=null)
            product.Category=await categoryService.getByID(product.Categoryid);
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
                var extension = Path.GetExtension(product.ImagePath!.FileName);
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
            var categories = await categoryService.getAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductResponse product)
        {
            var categories= await categoryService.getAll();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }

            if (product.ImagePath != null)
            {
                var extension = Path.GetExtension(product.ImagePath.FileName);
                var imageName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine($"{webHostEnvironment.WebRootPath}/images/product", imageName);
                using var stream = System.IO.File.Create(filePath);
                await product.ImagePath.CopyToAsync(stream);
                product.Image = imageName;
            }
            var res = await productService.update(product);

            if (res.Success)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                appLogger.LogInformation($"User with UserId: {userId} updated product: {product.Name}");

                return RedirectToAction(nameof(Index));
            }
            foreach (var err in res.Errors)
            {
                ModelState.AddModelError("", err);
            }
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await productService.delete(id);
         
            return RedirectToAction("Index");
        }


    }
}
