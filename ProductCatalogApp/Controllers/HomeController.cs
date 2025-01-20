using ecpmmerceApp.Application.Services;
using ecpmmerceApp.Application.Services.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogApp.Models;
using System.Diagnostics;

namespace ProductCatalogApp.Controllers
{
    public class HomeController(IAppLogger<HomeController> logger,IProductService productService) : Controller
    {

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Dashboard");
            var products =await productService.getActiveProducts();
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
