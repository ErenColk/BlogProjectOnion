using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;


namespace BlogProjectOnion.Application.Services.Concrete
{
    public class PostManager : BaseManager<Post>, IPostService
    {
        private readonly IBaseRepository<Post> _baseRepository;

        public PostManager(IBaseRepository<Post> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
    }
}
