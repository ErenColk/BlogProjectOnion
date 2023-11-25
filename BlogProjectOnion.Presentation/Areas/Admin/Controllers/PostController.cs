using Microsoft.AspNetCore.Mvc;

namespace BlogProjectOnion.Presentation.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
