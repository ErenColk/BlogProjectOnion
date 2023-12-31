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
    public class FollowManager : BaseManager<Follow>, IFollowService
    {
        private readonly IBaseRepository<Follow> _baseRepository;

        public FollowManager(IBaseRepository<Follow> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }



    }
}
