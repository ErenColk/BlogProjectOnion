using BlogProjectOnion.Application.Models.DTOs.CommentDTOs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectOnion.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IAppUserService appUserService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserService = appUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Index(LoginAppUserDTO loginAppUser)
        {
            AppUser user = await _userManager.FindByNameAsync(loginAppUser.UserName); 
            if(user == null) 
            {
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginAppUser.Password, loginAppUser.RememberMe, false);
            if (result.Succeeded)
            {
                bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                bool author = await _userManager.IsInRoleAsync(user, "Author");
                if( isAdmin)
                {
                return RedirectToAction("Index", "Home", new { area = "Admin" });

                }
                else
                return RedirectToAction("Index", "Home");
            }
            else
                return View();        


            
        }
        public async Task<IActionResult> Logout()
        {
           
            //AppUser ıd = await _userManager.GetUserAsync(HttpContext.User);

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
