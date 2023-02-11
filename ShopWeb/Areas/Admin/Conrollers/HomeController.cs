using Microsoft.AspNetCore.Mvc;

namespace ShopWeb.Areas.Admin.Conrollers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
