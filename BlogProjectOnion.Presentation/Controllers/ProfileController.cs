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

        public ProfileController(UserManager<AppUser> userManager, IAuthorService authorService, IMapper mapper, IPostService postService, ILikeService likeService, IAppUserService appUserService)
        {
            _userManager = userManager;
            _authorService = authorService;
            _mapper = mapper;
            _postService = postService;
            _likeService = likeService;
            _appUserService = appUserService;
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            AppUser appUser = new();

            if (id == Guid.Empty)
            {
                appUser = await _userManager.GetUserAsync(HttpContext.User);
                ViewData["IsUser"] = "true";
            }
            else
            {
                appUser = await _userManager.FindByIdAsync(id.ToString());
                TempData["id"] = id;
            }

            AppUserVM appUserVM = _mapper.Map<AppUserVM>(appUser);
          

            foreach (Like item in await _likeService.TGetByInclude(x => x.AppUserId == appUser.Id))
            {
                if (item.Post.Status != Status.Passive)
                    appUserVM.Posts.Add(await _postService.TGetDefault(x => x.Id == item.PostId));
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


            AuthorVM authorVM = _mapper.Map<AuthorVM>(await _authorService.TGetInclude(x => x.Id == id && x.Status == Domain.Enums.Status.Active || x.Status == Status.Modified, includes));
            foreach (var item in await _postService.TGetDefaults(x => x.Status == Status.Passive))
            {
                authorVM.Posts.Remove(item);

            }

            Author author = _mapper.Map<Author>(authorVM);

            AppUser appUser = await _appUserService.TGetDefault(x => x.Author.Id == id);
            appUser.Author = author;

            return View(appUser);
        }




        [HttpGet]
        public async Task<IActionResult> AddFollow(int id)
        {

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> RemoveFloow(int id)
        {


            return View();
        }


        //TODOO : USER LASTNAME FİRSTNAME EKLE APPUSER İÇERİSİNE
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
