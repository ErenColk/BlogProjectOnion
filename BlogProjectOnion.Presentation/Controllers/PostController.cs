using AutoMapper;
using BlogProjectOnion.Application.Models.DTOs.LikeDTOs;
using BlogProjectOnion.Application.Models.VMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAppUserService _appUserService;

        public PostController(IPostService postService, IMapper mapper, ICommentService commentService, UserManager<AppUser> userManager, ILikeService likeService)
        {
            _postService = postService;
            _mapper = mapper;
            _commentService = commentService;
            _userManager = userManager;
            _likeService = likeService;
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
            postVm.AppUserId = user.Id;
            Post post = await _postService.GetById(id);
            post.ClickCount += 1;
            await _postService.DefaultUpdate(post);
            return View(postVm);
        }

        [HttpGet]
        public async Task<IActionResult> PostList()
        {
            Expression<Func<Post, object>>[] includes = new Expression<Func<Post, object>>[]{

                x=> x.Author,x=>x.Comments,x=>x.Genre,x=>x.Likes
            };

            ViewData["renk"] = "blue";
            List<PostVM> postVm = _mapper.Map<List<PostVM>>(await _postService.GetIncludePost(x => x.Status != Domain.Enums.Status.Passive, includes)).OrderByDescending(x => x.ClickCount).Take(10).ToList();
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

    }
}
