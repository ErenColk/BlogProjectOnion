using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Concrete
{
    public class LikeManager : BaseManager<Like>, ILikeService
    {
        private readonly IBaseRepository<Like> _baseRepository;

        public LikeManager(IBaseRepository<Like> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
    }
}
