using AutoMapper;
using BlogProjectOnion.Application.Models.VMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProjectOnion.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,IPostService postService,IMapper mapper)
        {
            _logger = logger;
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<PostVM> postVm = _mapper.Map<List<PostVM>>(await _postService.TGetDefaults(x=>x.Status != Domain.Enums.Status.Passive)).OrderByDescending(x=> x.ClickCount).Take(10).ToList();
            return View(postVm);
        }

        public async Task<IActionResult> About()
        {

            return View();
        }
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