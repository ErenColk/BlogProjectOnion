using AutoMapper;
using BlogProjectOnion.Application.Models.DTOs.AuthorDTOs;
using BlogProjectOnion.Application.Models.VMs.AuthorVMs;
using BlogProjectOnion.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogProjectOnion.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> AuthorList()
        {
            List<ResultAuthorDTO> authorVM = _mapper.Map<List<ResultAuthorDTO>>(await _authorService.TGetDefaults());
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
            AuthorDetailVM authorDetail = await _authorService.TGetFilteredFirstOrDefault(select: x => new AuthorDetailVM
            {
                Id = id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CreatedDate = DateTime.Now,
                ImagePath = x.ImagePath,
                Posts = x.Posts,
                Status = x.Status,

            }, where: x => x.Id == id, null, q => q.Include(x => x.Posts));


            return View(authorDetail);
        }





    }
}
