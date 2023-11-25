using AutoMapper;
using BlogProjectOnion.Application.Helper;
using BlogProjectOnion.Application.Models.DTOs.AuthorDTOs;
using BlogProjectOnion.Application.Models.DTOs.CommentDTOs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectOnion.Presentation.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;

        public RegisterController(UserManager<AppUser> userManager, IMapper mapper, IAppUserService appUserService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appUserService = appUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterAppUserDTO registerAppUser)
        {
            if (ModelState.IsValid)
            {

                var result = await _appUserService.Register(registerAppUser);

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Hata", item.Description);
                }
                return RedirectToAction("Index","Home");

            }
            return View();
        }
    }
}


//string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Guid.NewGuid().ToString() + Path.GetExtension(registerAppUser.UploadPath.FileName));

//SaveImage.ImagePath(uploadPath, registerAppUser);

//registerAppUser.ImagePath = $"/images/{registerAppUser.UploadPath.FileName}";

//AppUser appUser = _mapper.Map<AppUser>(registerAppUser);
//IdentityResult result = await _userManager.CreateAsync(appUser, registerAppUser.Password);
//if (result.Succeeded)
//{

//    await _userManager.AddToRoleAsync(appUser, "User");

//    return RedirectToAction("Index", "Home");
//}