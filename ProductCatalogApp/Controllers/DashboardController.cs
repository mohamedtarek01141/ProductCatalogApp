using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
