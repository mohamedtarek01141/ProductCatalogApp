using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
