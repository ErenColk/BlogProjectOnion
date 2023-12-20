using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Concrete
{
    public class LikeManager : BaseManager<Like>, ILikeService
    {
        private readonly IBaseRepository<Like> _baseRepository;
        private readonly ILikeRepository _likeRepository;

        public LikeManager(IBaseRepository<Like> baseRepository,ILikeRepository likeRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _likeRepository = likeRepository;
        }

        public Task<List<Like>> TGetByInclude(Expression<Func<Like, bool>> expression = null, params Expression<Func<Like, object>>[]? includes)
        {
            return _likeRepository.GetByInclude(expression, includes);
        }
    }
}
