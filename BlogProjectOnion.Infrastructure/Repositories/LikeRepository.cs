using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.Repositories
{
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        private readonly AppDbContext _context;

        public LikeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Like>> GetByInclude(Expression<Func<Like, bool>> exp = null, params Expression<Func<Like, object>>[]? includes)
        {
            Expression<Func<Like, object>>[] defaultIncludes = new Expression<Func<Like, object>>[]
            {
               x=>x.Post,x=>x.AppUser
            };

            IQueryable<Like> likes = _context.Likes.AsQueryable();

            if(exp != null)
            {
                likes = likes.Where(exp);

            }

            if (includes.Length <= 0)
            {
                foreach (var item in defaultIncludes)
                {
                    likes = likes.Include(item);
                }

            }
            else
            {
                foreach (var item in includes)
                {
                    likes = likes.Include(item);
                }

            }



            return  await likes.ToListAsync();

        }

    }
}
