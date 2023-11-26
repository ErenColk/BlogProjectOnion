using Microsoft.AspNetCore.Mvc;

namespace BlogProjectOnion.Presentation.Areas.Admin.Controllers
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
