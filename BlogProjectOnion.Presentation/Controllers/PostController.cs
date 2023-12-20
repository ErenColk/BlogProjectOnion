using AutoMapper;
using BlogProjectOnion.Application.Helper;
using BlogProjectOnion.Application.Models.DTOs.LikeDTOs;
using BlogProjectOnion.Application.Models.DTOs.PostDTOs;
using BlogProjectOnion.Application.Models.VMs;
using BlogProjectOnion.Application.Models.VMs.PostVMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Application.ValidationRules.PostValidatonRules;
using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlogProjectOnion.Presentation.Controllers
{
    [Authorize(Roles = "Admin, User,Author")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILikeService _likeService;
        private readonly IGenreService _genreService;

        public PostController(IPostService postService, IMapper mapper, ICommentService commentService, UserManager<AppUser> userManager, ILikeService likeService, IGenreService genreService)
        {
            _postService = postService;
            _mapper = mapper;
            _commentService = commentService;
            _userManager = userManager;
            _likeService = likeService;
            _genreService = genreService;
        }

        //TODOO : BURAYI DÜZENLE
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            Expression<Func<Post, object>>[] includes = new Expression<Func<Post, object>>[]{

                x=> x.Author,x=>x.Comments,x=>x.Genre,x=>x.Likes
            };

            PostVM postVm = _mapper.Map<PostVM>(await _postService.GetByIncludePost(x => x.Id == id, includes));
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            postVm.AppUser = user;
            Post post = await _postService.GetById(id);
            post.ClickCount += 1;
            await _postService.TDefaultUpdate(post);
            return View(postVm);
        }

        [HttpGet]
        public async Task<IActionResult> PostList()
        {
            Expression<Func<Post, object>>[] includes = new Expression<Func<Post, object>>[]{

                x=> x.Author,x=>x.Comments,x=>x.Genre,x=>x.Likes
            };


            List<PostVM> postVm = _mapper.Map<List<PostVM>>(await _postService.GetIncludePost(x => x.Status != Domain.Enums.Status.Passive, includes)).Take(10).ToList();
            //.Skip((pagenumber - 1) * 10) // SAFYA ATLAMA METODU
            
            return View(postVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddLike(CreateLikeDTO createLikeDTO)
        {
            Like like = _mapper.Map<Like>(createLikeDTO);
            await _likeService.TCreate(like);
            return Ok("OK");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveLike(DeleteLikeDTO deleteLikeDTO)
        {
            Like like = await _likeService.TGetDefault(x => x.PostId == deleteLikeDTO.PostId && x.AppUserId == deleteLikeDTO.UserId);
            await _likeService.THardDelete(like);
            return Ok("OK");
        }
        [HttpGet]
        public async Task<IActionResult> ListComments(int id)
        {
            List<CommentVM> commentVM = _mapper.Map<List<CommentVM>>(await _commentService.GetCommentThenInclude(x => x.PostId == id));

            if (commentVM.Count > 0)
            {
                return PartialView("_CommentListPartialView", commentVM);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CommentPost(PostVM postVM)
        {
            if (postVM != null)
            {
                Comment comment = new Comment()
                {
                    AppUserId = postVM.AppUserId,
                    Content = postVM.Comment,
                    PostId = postVM.Id
                };
                await _commentService.TCreate(comment);
                return RedirectToAction("ListComments", new { id = postVM.Id });
            }
            return View();
        }


        //[Authorize(Roles = "Admin,Author")]
        [HttpGet]
        public async Task<IActionResult> MyPostList(Guid id)
        {
            AppUser appUser;
            if (id == Guid.Empty)
            {

                appUser = await _userManager.Users.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == _userManager.GetUserAsync(HttpContext.User).Result.Id);
            }
            else
            {
                appUser = await _userManager.Users.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
            }

            List<ResultPostDTO> resultPostDTO = _mapper.Map<List<ResultPostDTO>>(await _postService.TGetDefaults(x => x.AuthorId == appUser.Author.Id && x.Status != Domain.Enums.Status.Passive));

            return View(resultPostDTO);

        }

        [HttpGet]
        public async Task<IActionResult> AddPost()
        {

            AppUser appUser = await _userManager.Users.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == _userManager.GetUserAsync(HttpContext.User).Result.Id);

            CreatePostVM createPostVM = new CreatePostVM()
            {
                Genres = await _genreService.TGetDefaults(),
                AuthorId = appUser.Author.Id
            };

            return View(createPostVM);

        }

        [HttpPost]
        public async Task<IActionResult> AddPost(CreatePostVM createPostVM)
        {

            PostCreateValidator validationRules = new PostCreateValidator();
            var resultCreate = validationRules.Validate(createPostVM);

            if (resultCreate.IsValid)
            {

                Post post = _mapper.Map<Post>(createPostVM);
                if (createPostVM.UploadPath != null)
                {
                    post.ImagePath = $"images/{Guid.NewGuid()}" + Path.GetExtension(createPostVM.UploadPath.FileName);
                    SaveImage.ImagePath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", post.ImagePath), createPostVM);
                }

                bool result = await _postService.TCreate(post);
                if (!result)
                {
                    return View(createPostVM);
                }
                else
                    return RedirectToAction("MyPostList", "Post");

            }
            else
            {
                foreach (var item in resultCreate.Errors)
                {

                    ModelState.AddModelError("", item.ErrorMessage);

                }
                createPostVM.Genres = await _genreService.TGetDefaults();
                return View(createPostVM);
            }


        }



        [HttpGet]
        public async Task<IActionResult> GenreList()
        {
            List<Genre> genres = _mapper.Map<List<Genre>>(await _genreService.TGetDefaults());
            return PartialView("_GenresPartialView", genres);

        }

    }
}
