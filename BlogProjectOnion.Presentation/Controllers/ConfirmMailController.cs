using AutoMapper;
using BlogProjectOnion.Application.Models.DTOs.MailDtos;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectOnion.Presentation.Controllers
{
    public class ConfirmMailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;

        public ConfirmMailController(UserManager<AppUser> userManager, IMapper mapper, IAppUserService appUserService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appUserService = appUserService;
         
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id.ToString());

            ConfirmMailDTO confirmMailDTO = new ConfirmMailDTO()
            {
                Email = appUser.Email,
                Id = id
            };

            return View(confirmMailDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailDTO confirmMailDTO)
        {
            var user = await _userManager.FindByEmailAsync(confirmMailDTO.Email);

            if (user.ConfirmCode == confirmMailDTO.ConfirmCode)
            {
                user.EmailConfirmed = true;
                user.Status = Domain.Enums.Status.Active;
                await _appUserService.TDefaultUpdate(user);                    

                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.AddModelError("", "Kodu tekrar giriniz!");
                confirmMailDTO.ConfirmCode = 0;
                return View(confirmMailDTO);

            }

        }


    }
}
