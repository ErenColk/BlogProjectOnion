using Microsoft.AspNetCore.Mvc;

namespace BlogProjectOnion.Presentation.Controllers
{
    public class PostController : Controller
    {
        public PostController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
