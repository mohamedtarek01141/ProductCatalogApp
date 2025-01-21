using ecpmmerceApp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogApp.Models;
using System.Diagnostics;

namespace ProductCatalogApp.Controllers
{
    public class HomeController(IProductService productService,ICategoryService categoryService) : Controller
    {

        public async Task<IActionResult> Index(string searchQuery, Guid? categoryFilter)
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Dashboard");
            var products = await productService.getActiveProducts();
            if (categoryFilter!=null)
            {
                products=products.Where(p=>p.Categoryid==categoryFilter);
               
            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p => p.Name != null && p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }


            ViewBag.Categories = await categoryService.getAll();
            return View(products);
        }
        [Authorize]

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
