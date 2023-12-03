using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Author> GetInclude(Expression<Func<Author, bool>> expression , params Expression<Func<Author, object>>[] includes)
        {
            IQueryable<Author> query = _context.Authors.AsQueryable();


            foreach (var item in includes)
            {

                query = query.Include(item);

            }

     
            return  await query.FirstOrDefaultAsync(expression);



        }
    }
}
