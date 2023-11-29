using AutoMapper;
using BlogProjectOnion.Application.Models.VMs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BlogProjectOnion.Presentation.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService,IMapper mapper )
        {
            _postService = postService;
            _mapper = mapper;
        }

        //TODOO : BURAYI DÜZENLE
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            Expression<Func<Post, object>>[] includes = new Expression<Func<Post, object>>[]{
                
                x=> x.Author,x=>x.Comments,x=>x.Genre,x=>x.Likes
            };

            PostVM postVm = _mapper.Map<PostVM>(await _postService.GetByIncludePost(x=>x.Id == id, includes));
            Post post = await _postService.GetById(id);
            post.ClickCount += 1;
            await _postService.DefaultUpdate(post);
            return View(postVm);
        }



    }
}
