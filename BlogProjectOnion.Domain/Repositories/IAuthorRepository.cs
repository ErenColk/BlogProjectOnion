using BlogProjectOnion.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Repositories
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {

        Task<Author> GetInclude(Expression<Func<Author,bool>> expression,params Expression<Func<Author, object>>[] includes);
    }
}
