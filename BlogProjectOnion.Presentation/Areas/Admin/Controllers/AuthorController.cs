using AutoMapper;
using BlogProjectOnion.Application.Models.DTOs.AppUserDTOs;
using BlogProjectOnion.Application.Models.DTOs.AuthorDTOs;
using BlogProjectOnion.Application.Models.VMs.AuthorVMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogProjectOnion.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;

        public AuthorController(IAuthorService authorService, IMapper mapper,IAppUserService appUserService)
        {
            _authorService = authorService;
            _mapper = mapper;
            _appUserService = appUserService;
        }

        public async Task<IActionResult> AuthorList()
        {
            List<ResultAuthorDTO> authorVM = _mapper.Map<List<ResultAuthorDTO>>(await _authorService.TGetDefaults(x=>x.Id != 1 && x.Status != Domain.Enums.Status.Passive));
            if (authorVM == null)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View(authorVM);
        }

        //TODOO : BURAYI YAP
        [HttpGet]
        public async Task<IActionResult> DetailAuthor(int id)
        {
            AppUser appUser = await _appUserService.TGetDefault(x => x.Author.Id == id);

            AuthorDetailVM authorDetail = await _authorService.TGetFilteredFirstOrDefault(select: x => new AuthorDetailVM
            {
                Id = id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CreatedDate = DateTime.Now,
                ImagePath = x.ImagePath,
                Posts = x.Posts,
                Status = x.Status,
                AppUserId = appUser.Id

            }, where: x => x.Id == id, null, q => q.Include(x => x.Posts));


            return View(authorDetail);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            Author author = await _authorService.GetById(id);

            if(author == null)
            {
                return RedirectToAction("AuthorList", "Author");
            }
            else
            {
                AuthorDetailVM detailVM = _mapper.Map<AuthorDetailVM>(author);
                return View(detailVM);
            }
        }




        [HttpGet]
        public async Task<IActionResult> HardDeleteAuthor(int id)
        {
            Author author = await _authorService.GetById(id);
            if(author == null)
            {
                return RedirectToAction("AuthorList", "Author");

            }
            else
            {
                await _authorService.TDelete(author);
                return View(author);
            }

        }


        [HttpGet]
        public async Task<IActionResult> DeletedListAuthor()
        {
            List<AuthorDetailVM> result = _mapper.Map<List<AuthorDetailVM>>(await _authorService.TGetDefaults(x => x.Status == Status.Passive));
            return View(result);

        }


        [HttpGet]
        public async Task<IActionResult> AddAgainAuthor(int id)
        {
            Author updateAuthor = await _authorService.GetById(id);

            if (updateAuthor == null)
            {
                return View();
            }
            else
            {
                updateAuthor.Status = Status.Active;
                await _authorService.TUpdate(updateAuthor);
                return RedirectToAction("AuthorList", "Author");
            }

        }


    }
}
