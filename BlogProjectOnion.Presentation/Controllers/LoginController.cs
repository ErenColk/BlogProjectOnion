using BlogProjectOnion.Application.Models.DTOs.CommentDTOs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Application.ValidationRules.AppUserValidationRules;
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
            LoginAppUserDTO loginAppUser = new LoginAppUserDTO();
            loginAppUser.IsEmailConfirmed = true;         
            return View(loginAppUser);
        }

        //MAİL ONAYLANMA SAYFASINA YÖNLENDİR
        [HttpPost]
        public async Task<IActionResult> Index(LoginAppUserDTO loginAppUser)
        {
            AppUserLoginValidator validationRules = new AppUserLoginValidator();
            var resultLogin = validationRules.Validate(loginAppUser);
            loginAppUser.IsEmailConfirmed = true;

            if (resultLogin.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(loginAppUser.UserName);

                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginAppUser.UserName, loginAppUser.Password, loginAppUser.RememberMe, true);

                if (!result.IsLockedOut)
                {
                    if (result.Succeeded)
                    {
                        if (user.EmailConfirmed == true)
                        {
                            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                            bool author = await _userManager.IsInRoleAsync(user, "Author");
                            await _signInManager.SignInAsync(user, isPersistent: loginAppUser.RememberMe);
                            if (isAdmin)
                            {
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                            else
                                return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Mail Adresinizi Onaylamanız Gerekmektedir.");
                            loginAppUser.IsEmailConfirmed = false;
                            var appUser = await _userManager.FindByNameAsync(loginAppUser.UserName);
                            loginAppUser.Id = appUser.Id;
                            await _signInManager.SignOutAsync();

                            return View(loginAppUser);
                        }
                    }
                    else if (user != null && user.AccessFailedCount >= 4)
                    {
                        //Yanlış şifre girişinde hesabı kilitleme süresi
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddMinutes(1));
                        await _userManager.UpdateAsync(user);
                    }
                    ModelState.AddModelError("", "Kullanıcı adı veya şifreniz hatalı");
                    return View(loginAppUser);
                }
                else
                {
                    ModelState.AddModelError("", "Çok fazla yanlış şifre girdiniz.Kısa bir süre sonra tekrar deneyiniz");
                    return View(loginAppUser);
                }
            }
            else
            {
                foreach (var item in resultLogin.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(loginAppUser);
            }
        }
        public async Task<IActionResult> Logout()
        {

            //AppUser ıd = await _userManager.GetUserAsync(HttpContext.User);

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
