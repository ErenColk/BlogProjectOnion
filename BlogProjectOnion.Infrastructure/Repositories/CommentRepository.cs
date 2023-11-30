using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment> , ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<List<Comment>> GetCommentThenInclude(Expression<Func<Comment, bool>> expression = null)
		{
            if(expression == null)
            {
               return await _context.Comments.Include(x=>x.AppUser).ThenInclude(x=>x.Author).ToListAsync();
            }
            else
				return await _context.Comments.Where(expression).Include(x => x.AppUser).ThenInclude(x => x.Author).ToListAsync();




		}
	}
}
