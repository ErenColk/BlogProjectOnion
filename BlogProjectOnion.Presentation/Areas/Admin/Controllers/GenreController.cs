using AutoMapper;
using BlogProjectOnion.Application.Models.DTOs.GenreDTOs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using BlogProjectOnion.Domain.Enums;
namespace BlogProjectOnion.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()

        {
            var result = _mapper.Map<List<ResultGenreDTO>>(await _genreService.TGetDefaults(x => x.Status == Status.Active || x.Status == Status.Modified));
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> DeletedListGenre()
        {
            var result = _mapper.Map<List<DeletedResultGenreDTO>>(await _genreService.TGetDefaults(x => x.Status == Status.Passive));
            return View(result);

        }

        [HttpGet]
        public async Task<IActionResult> AddAgainGenre(int id)
        {
            Genre updateGenre = await _genreService.GetById(id);
            if (updateGenre == null)
            {

                return View();
            }
            else
            {

                updateGenre.Status = Status.Active;
                await _genreService.TUpdate(updateGenre);
                return RedirectToAction("Index", "Genre");
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddGenre()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(CreateGenreDTO genreDTO)
        {
            Genre genre = _mapper.Map<Genre>(genreDTO);

            if (genre == null)
            {
                return View();
            }
            else
            {
                await _genreService.TCreate(genre);

                return RedirectToAction("Index", "Genre");
            }

        }

        [HttpGet]
        public async Task<IActionResult> UpdateGenre(int id)
        {
            UpdateGenreDTO updateGenreDTO = _mapper.Map<UpdateGenreDTO>(await _genreService.GetById(id));
            if (updateGenreDTO == null)
                return View();
            else
                return View(updateGenreDTO);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGenre(UpdateGenreDTO updateGenreDTO)
        {

            Genre genre = await _genreService.GetById(updateGenreDTO.Id);

            if (genre == null)
                return View();
            else
            {
                _mapper.Map(updateGenreDTO, genre);
                await _genreService.TUpdate(genre);
                return RedirectToAction("Index", "Genre");
            }

        }

        [HttpGet]
        public async Task<IActionResult> DetailGenre(int id)
        {

            Genre genre = await _genreService.GetById(id);

            if (genre == null)
                return RedirectToAction("Index", "Genre");
            else
            {
                DetailGenreDTO detailGenreDTO = _mapper.Map<DetailGenreDTO>(genre);
                return View(detailGenreDTO);
            }

        }

        [HttpGet]
        public async Task<IActionResult> DeleteGenre(int id)
        {

            Genre genre = await _genreService.GetById(id);

            if (genre == null)
                return RedirectToAction("Index", "Genre");
            else
            {
                DetailGenreDTO detailGenreDTO = _mapper.Map<DetailGenreDTO>(genre);
                return View(detailGenreDTO);
            }

        }

        [HttpGet]
        public async Task<IActionResult> HardDeleteGenre(int id)
        {

            Genre genre = await _genreService.GetById(id);

            if (genre == null)
                return RedirectToAction("Index", "Genre");
            else
            {
                await _genreService.TDelete(genre);
                DetailGenreDTO detailGenreDTO = _mapper.Map<DetailGenreDTO>(genre);
                return View(detailGenreDTO);
            }
        }




    }
}
