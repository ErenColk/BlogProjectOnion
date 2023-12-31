
using AutoMapper;
using BlogProjectOnion.Application.Helper;
using BlogProjectOnion.Application.Models.DTOs.AppUserDTOs;
using BlogProjectOnion.Application.Models.DTOs.CommentDTOs;
using BlogProjectOnion.Application.Models.VMs.AppUserVMs;
using BlogProjectOnion.Application.Models.VMs.AuthorVMs;
using BlogProjectOnion.Application.Models.VMs.PostVMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq.Expressions;

namespace BlogProjectOnion.Presentation.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly ILikeService _likeService;
        private readonly IAppUserService _appUserService;
        private readonly IFollowService _followService;

        public ProfileController(UserManager<AppUser> userManager, IAuthorService authorService, IMapper mapper, IPostService postService, ILikeService likeService, IAppUserService appUserService, IFollowService followService)
        {
            _userManager = userManager;
            _authorService = authorService;
            _mapper = mapper;
            _postService = postService;
            _likeService = likeService;
            _appUserService = appUserService;
            _followService = followService;
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            AppUser appUser = new();

            if (id == Guid.Empty)
            {
                appUser = await _userManager.GetUserAsync(HttpContext.User);
                ViewData["IsUser"] = false;
                ViewData["IsOpenedSettings"] = "true";
            }
            else
            {
                appUser = await _userManager.FindByIdAsync(id.ToString());
                TempData["id"] = id;
                ViewData["IsUser"] = true;
            }

            AppUserVM appUserVM = _mapper.Map<AppUserVM>(appUser);

            foreach (Like item in await _likeService.TGetByInclude(x => x.AppUserId == appUser.Id))
            {
                if (item.Post.Status != Status.Passive)
                    appUserVM.Posts.Add(await _postService.TGetDefault(x => x.Id == item.PostId));
            }

            foreach (Follow item in await _followService.TGetDefaults(x=>x.AppUserId == appUser.Id))
            {
                item.Author = await _authorService.TGetFilteredInclude(x=>x.Id == item.AuthorId,include: q=>q.Include(x=>x.Posts));
                appUserVM.Follows.Add(item);

            }


            return View(appUserVM);
        }

        [HttpGet]
        public async Task<IActionResult> ListPost(int id)
        {
            AppUser appUser = new();
            if (TempData["id"] == "" || TempData["id"] == null)
            {
                appUser = await _userManager.GetUserAsync(HttpContext.User);

            }
            else
            {
                appUser = await _userManager.FindByIdAsync(TempData["id"].ToString());

            }

            List<Post> posts = new List<Post>();

            foreach (Like item in await _likeService.TGetDefaults(x => x.AppUserId == appUser.Id))
            {
                posts.Add(await _postService.TGetDefault(x => x.Id == item.PostId));
            }
            return PartialView("_PostListPartialView", posts.Where(x => x.Status != Status.Passive).Skip((id - 1) * 5).Take(5).ToList());
        }


        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            Expression<Func<Author, object>>[] includes = new Expression<Func<Author, object>>[]
            {
                x=>x.Posts,x=>x.Follows
            };

            Author author = _mapper.Map<Author>(await _authorService.TGetInclude(x => x.Id == id && (x.Status != Status.Passive), includes));

            foreach (var item in await _postService.TGetDefaults(x => x.Status == Status.Passive))
            {
                author.Posts.Remove(item);
            }
            AppUser appUser = await _appUserService.TGetDefault(x => x.Author.Id == id);
            appUser.Author = author;


            AppUser loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (appUser.Author.Follows.Where(x => x.AppUserId == loggedInUser.Id).ToList().Count <= 0)
            {
                ViewData["InFollow"] = false;
            }
            else
            {
                ViewData["InFollow"] = true;
            }
            return View(appUser);

        }




        [HttpPost]
        public async Task<IActionResult> AddFollow(int id)
        {
            AppUser appUser = await _userManager.GetUserAsync(HttpContext.User);

            if (appUser != null)
            {
                Follow follow = new Follow()
                {
                    AppUserId = appUser.Id,
                    AuthorId = id
                };

                await _followService.TCreate(follow);
            }
            return RedirectToAction("Index", new { id = id });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFollow(int id)
        {

            AppUser appUser = await _userManager.GetUserAsync(HttpContext.User);

            if (appUser != null)
            {
                Follow follow = await _followService.TGetDefault(x => x.AppUserId == appUser.Id && x.AuthorId == id);
                await _followService.THardDelete(follow);

            }

            return RedirectToAction("Index", new { id = id });
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePrivate(UpdateAppUserPersonalDTO appUserDTO)
        {

            AppUser appUser = await _appUserService.TGetFilteredInclude(x => x.Id == appUserDTO.Id, q => q.Include(x => x.Author));

            _mapper.Map(appUserDTO, appUser);

            if (appUserDTO.UploadPath != null)
            {
                appUser.ImagePath = $"images/{Guid.NewGuid()}" + Path.GetExtension(appUserDTO.UploadPath.FileName);

                SaveImage.ImagePath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", appUser.ImagePath), appUserDTO);
            }

            bool isInRole = await _userManager.IsInRoleAsync(appUser, "Author");
            if (isInRole)
            {
                appUser.Author.FirstName = appUser.FirstName;
                appUser.Author.LastName = appUser.LastName;
                appUser.Author.ImagePath = appUser.ImagePath;
            }

            await _userManager.UpdateAsync(appUser);
            return RedirectToAction("Detail", "Profile");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateAppUserContactDTO appUserDTO)
        {
            AppUser appUser = await _appUserService.TGetFilteredInclude(x => x.Id == appUserDTO.Id, q => q.Include(x => x.Author));

            _mapper.Map(appUserDTO, appUser);
            await _userManager.UpdateAsync(appUser);

            return RedirectToAction("Detail", "Profile");
        }
    }
}
