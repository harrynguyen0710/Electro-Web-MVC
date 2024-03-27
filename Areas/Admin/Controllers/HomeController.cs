using Microsoft.AspNetCore.Mvc;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Admmin()
        {
            return View();
        }

        public IActionResult ProductsView()
        {
            return View();  
        }
        public IActionResult DeviceView()
        {
            return View();
        }

    }
}
