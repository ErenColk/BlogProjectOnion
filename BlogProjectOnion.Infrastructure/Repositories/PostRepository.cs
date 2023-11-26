using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Post> GetByIncludePost(Expression<Func<Post, bool>> expression, params Expression<Func<Post, object>>[] include)
        {

            IQueryable<Post> query = _context.Posts.AsQueryable();

            if (include.Any())
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }

            return await query.Where(expression).FirstOrDefaultAsync();

        }

        public async Task<List<Post>> GetIncludePost(Expression<Func<Post, bool>> expression = null, params Expression<Func<Post, object>>[] include)
        {

            IQueryable<Post> query = _context.Posts.AsQueryable();
            if (expression != null)
            {

                query = query.Where(expression);

            }

            if (include.Any())
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }

            return await query.ToListAsync();

        }
    }
}
