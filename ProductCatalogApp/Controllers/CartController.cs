using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
