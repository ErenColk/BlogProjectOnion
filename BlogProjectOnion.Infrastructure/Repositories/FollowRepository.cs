using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.Repositories
{
    public class FollowRepository : BaseRepository<Follow> , IFollowRepository
    {
        private readonly AppDbContext _context;

        public FollowRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }




    }
}
