using BlogProjectOnion.Application.Models.VMs.StatisticsVMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectOnion.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly IAppUserService _appUserService;
        private readonly IPostService _postService;

        public StatisticsController(IAuthorService authorService, IGenreService genreService, IAppUserService appUserService, IPostService postService)
        {
            _authorService = authorService;
            _genreService = genreService;
            _appUserService = appUserService;
            _postService = postService;
        }


        //TODOO: İSTATİSTİKLERİ AYARLA
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ResultStatisticsVM statisticsVM = new ResultStatisticsVM()
            {

                Posts = await _postService.TGetFilteredList(select: x => new Post
                {
                    CreatedDate = x.CreatedDate,
                    Author = x.Author,
                    ClickCount = x.ClickCount,
                    Title = x.Title,
                }, where: x => x.Status != Status.Passive, null, include: q => q.Include(x => x.Author)),
                Authors = await _authorService.TGetDefaults(x => x.Status != Status.Passive && x.FirstName != "Admin"),
                AppUsers = await _appUserService.TGetDefaults(x => x.Status != Status.Passive && x.Author == null)
            };

            statisticsVM.Top10Posts = statisticsVM.Posts.OrderByDescending(x => x.ClickCount).Take(10).ToList();

            foreach (var item in statisticsVM.Posts)
            {
                statisticsVM.TotalRead += item.ClickCount;
            }

            return View(statisticsVM);
        }

        [HttpGet]
        public async Task<IActionResult> HandleCountPost()
        {
            int[] day = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] count = new int[12];

            List<Post> posts = new List<Post>();
            for (int i = 0; i < 12; i++)
            {
                posts = await _postService.TGetDefaults(x => x.CreatedDate.Month == (i + 1) && x.CreatedDate.Year == DateTime.Now.Year);
                count[i] = posts.Count;
            }

            return Ok(count);
        }


    }
}
