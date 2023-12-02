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

        public ProfileController(UserManager<AppUser> userManager,IAuthorService authorService,IMapper mapper)
        {
            _userManager = userManager;
            _authorService = authorService;
            _mapper = mapper;
        }
        //TODOO : PROFİL KISMINI HALLET
        //TAKİP ETME EKLE. 
        //yorumları ve tabloyu düzenle
        public async Task<IActionResult> Detail()
        {
            AppUser appUser =  await _userManager.GetUserAsync(HttpContext.User);

            AppUserVM appUserVM = _mapper.Map<AppUserVM>(appUser);


            return View(appUser);
        }
    }
}
