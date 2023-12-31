using AutoMapper;
using BlogProjectOnion.Application.Models.DTOs.AppUserDTOs;
using BlogProjectOnion.Application.Models.DTOs.GenreDTOs;
using BlogProjectOnion.Application.Models.VMs.AuthorVMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectOnion.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IAppUserService appUserService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            IList<AppUser> app = await _userManager.GetUsersInRoleAsync("User");
            List<ResultAppUserDTO> resultAppUserDTO = _mapper.Map<List<ResultAppUserDTO>>(app.Where(x => x.Status != Status.Passive));
            return View(resultAppUserDTO);
        }

        [HttpGet]
        public async Task<IActionResult> DetailUser(Guid id)
        {
            DetailAppUserDto detailAppUserDto = _mapper.Map<DetailAppUserDto>(await _userManager.FindByIdAsync(id.ToString()));

            return View(detailAppUserDto);
        }



        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id.ToString());

            if (appUser == null)
            {
                return RedirectToAction("UserList", "User");
            }
            else
            {
                DetailAppUserDto detailVM = _mapper.Map<DetailAppUserDto>(appUser);
                return View(detailVM);
            }
        }


        [HttpGet]
        public async Task<IActionResult> HardDeleteUser(Guid id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                return RedirectToAction("UserList", "User");
            }
            else
            {
                await _appUserService.TDelete(appUser);
                return View(appUser);
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeletedListUser()
        {
            List<DetailAppUserDto> result = _mapper.Map<List<DetailAppUserDto>>(await _appUserService.TGetDefaults(x => x.Status == Status.Passive));
            return View(result);

        }


        [HttpGet]
        public async Task<IActionResult> AddAgainUser(Guid id)
        {
            AppUser updateUser = await _userManager.FindByIdAsync(id.ToString());

            if (updateUser == null)
            {
                return View();
            }
            else
            {
                updateUser.Status = Status.Active;
                await _appUserService.TUpdate(updateUser);
                return RedirectToAction("UserList", "User");
            }

        }

    }
}
