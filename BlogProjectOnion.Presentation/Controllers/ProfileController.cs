using AutoMapper;
using BlogProjectOnion.Application.Models.VMs.AppUserVMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectOnion.Presentation.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly ILikeService _likeService;

        public ProfileController(UserManager<AppUser> userManager, IAuthorService authorService, IMapper mapper, IPostService postService, ILikeService likeService)
        {
            _userManager = userManager;
            _authorService = authorService;
            _mapper = mapper;
            _postService = postService;
            _likeService = likeService;
        }
        //TODOO : PROFİL KISMINI HALLET
        //TAKİP ETME EKLE. 
        //yorumları ve tabloyu düzenle
        public async Task<IActionResult> Detail()
        {
            AppUser appUser = await _userManager.GetUserAsync(HttpContext.User);

            AppUserVM appUserVM = _mapper.Map<AppUserVM>(appUser);
            //Beğenilen Makaleler
            foreach (Like item in await _likeService.TGetDefaults(x => x.AppUserId == appUser.Id))
            {
                appUserVM.Posts.Add(await _postService.TGetDefault(x => x.Id == item.PostId));
            }

            return View(appUserVM);
        }

        [HttpGet]
        public async Task<IActionResult> ListPost(int id)
        {
            AppUser appUser = await _userManager.GetUserAsync(HttpContext.User);
            List<Post> posts = new List<Post>();

            foreach (Like item in await _likeService.TGetDefaults(x => x.AppUserId == appUser.Id))
            {
                posts.Add(await _postService.TGetDefault(x => x.Id == item.PostId));
            }         
            return PartialView("_PostListPartialView", posts.Skip((id - 1) * 5).Take(5).ToList());
        }
    }
}
