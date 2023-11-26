using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using System.Linq.Expressions;

namespace BlogProjectOnion.Application.Services.Concrete
{
    public class PostManager : BaseManager<Post>, IPostService
    {
        private readonly IBaseRepository<Post> _baseRepository;
        private readonly IPostRepository _postRepository;

        public PostManager(IBaseRepository<Post> baseRepository,IPostRepository postRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _postRepository = postRepository;
        }

        public Task<Post> GetByIncludePost(Expression<Func<Post, bool>> expression = null, params Expression<Func<Post, object>>[] include)
        {
            return _postRepository.GetByIncludePost(expression, include);
        }

        public Task<List<Post>> GetIncludePost(Expression<Func<Post, bool>> expression = null, params Expression<Func<Post, object>>[] include)
        {
            return _postRepository.GetIncludePost(expression, include);
        }
    }
}
