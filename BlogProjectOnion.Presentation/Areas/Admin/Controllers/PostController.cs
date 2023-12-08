using AutoMapper;
using BlogProjectOnion.Application.Helper;
using BlogProjectOnion.Application.Models.DTOs.GenreDTOs;
using BlogProjectOnion.Application.Models.DTOs.PostDTOs;
using BlogProjectOnion.Application.Models.VMs;
using BlogProjectOnion.Application.Models.VMs.PostVMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Application.ValidationRules.PostValidatonRules;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlogProjectOnion.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly IGenreService _genreService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthorService _authorService;

        public PostController(IPostService postService, IMapper mapper, ICommentService commentService, IGenreService genreService, UserManager<AppUser> userManager, IAuthorService authorService)
        {
            _postService = postService;
            _mapper = mapper;
            _commentService = commentService;
            _genreService = genreService;
            _userManager = userManager;
            _authorService = authorService;
        }
        public async Task<IActionResult> PostList()
        {
            var result = _mapper.Map<List<PostVM>>(await _postService.TGetFilteredList(
             select: x => new PostVM
             {
                 Id = x.Id,
                 Title = x.Title,
                 SubTitle = x.SubTitle,
                 Genre = x.Genre,
                 Content = x.Content,
                 Likes = x.Likes,
                 ClickCount = x.ClickCount,
             },
             where: x => x.Status == Status.Active || x.Status == Status.Modified, null,
             include: q => q.Include(x => x.Likes).Include(x => x.Genre)));

            return View(result);
        }


        //TODOO BURAYA GÖZ AT ID YI FARKLI ÇEKMEYE ÇALIŞ
        [HttpGet]
        public async Task<IActionResult> AddPost()
        {

            //AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            //AppUser appUser = await _userManager.Users.Include(x => x.Author).Where(x => x == user).FirstOrDefaultAsync();

            //await _userManager.GetUserAsync(HttpContext.User);
            //appUser.Author = _authorService.TGetDefault(x=>x)


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
                    return RedirectToAction("PostList", "Post");

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
        public async Task<IActionResult> DeletedListPost()
        {
            var result = _mapper.Map<List<ResultPostDTO>>(await _postService.TGetFilteredList(
             select: x => new ResultPostDTO
             {
                 Id = x.Id,
                 Title = x.Title,
                 Content = x.Content,
                 CreatedDate = x.CreatedDate,
                 DeletedDate = x.DeletedDate,
                 AuthorFullName = x.Author.FirstName + x.Author.LastName,
                 Status = x.Status
             },
             where: x => x.Status == Status.Passive, null, null));


            return View(result);

        }

        [HttpGet]
        public async Task<IActionResult> DetailPost(int id)
        {
            PostDetailVM result = _mapper.Map<PostDetailVM>(await _postService.TGetFilteredFirstOrDefault(
             select: x => new PostDetailVM
             {
                 Id = x.Id,
                 Title = x.Title,
                 SubTitle = x.SubTitle,
                 Genre = x.Genre,
                 Content = x.Content,
                 Likes = x.Likes,
                 ClickCount = x.ClickCount,
                 CreatedDate = x.CreatedDate,
                 UpdatedDate = x.UpdatedDate,
                 Status = x.Status,
                 Author = x.Author,
                 Comments = x.Comments,

             },
             where: x => (x.Status == Status.Active || x.Status == Status.Modified) && x.Id == id, null,
             include: q => q.Include(x => x.Likes).Include(x => x.Genre)));

            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> PostDetailComments(int id)
        {
            List<PostDetailCommentVM> comments = await _commentService.TGetFilteredList(select: x => new PostDetailCommentVM
            {
                Comment = x,

            }, where: x => x.PostId == id, null, include: q => q.Include(x => x.AppUser));
            return PartialView("_CommentListPartialView", comments);
        }

        [HttpGet]
        public async Task<IActionResult> AddAgainPost(int id)
        {
            Post updatePost = await _postService.GetById(id);
            if (updatePost == null)
            {

                return View();
            }
            else
            {

                updatePost.Status = Status.Active;
                await _postService.TUpdate(updatePost);
                return RedirectToAction("PostList", "Post");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeletePost(int id)
        {

            Post post = await _postService.GetById(id);

            if (post == null)
                return RedirectToAction("PostList", "Post");
            else
            {
                ResultPostDTO deletePost = _mapper.Map<ResultPostDTO>(post);
                return View(deletePost);
            }
        }

        [HttpGet]
        public async Task<IActionResult> HardDeletePost(int id)
        {

            Post post = await _postService.GetById(id);

            if (post == null)
                return RedirectToAction("PostList", "Post");
            else
            {
                await _postService.TDelete(post);
                ResultPostDTO deletePost = _mapper.Map<ResultPostDTO>(post);
                return View(deletePost);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePost(int id)
        {

            UpdatePostDTO updatePost = _mapper.Map<UpdatePostDTO>(await _postService.GetById(id));
            updatePost.Genres = await _genreService.TGetDefaults();
            if (updatePost == null)
                return RedirectToAction("PostList", "Post");
            else
                return View(updatePost);

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(UpdatePostDTO updatePost)
        {

            Post post = await _postService.GetById(updatePost.Id);

            if (post == null)
                return View();
            else
            {
                if (updatePost.UploadPath != null)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", post.ImagePath);

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    updatePost.ImagePath = $"images/{Guid.NewGuid()}" + Path.GetExtension(updatePost.UploadPath.FileName);
                    SaveImage.ImagePath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", updatePost.ImagePath), updatePost);

                }

                _mapper.Map(updatePost, post );
                await _postService.TUpdate(post);
                return RedirectToAction("PostList", "Post");
            }



        }

    }
}
