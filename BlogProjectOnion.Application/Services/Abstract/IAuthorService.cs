using BlogProjectOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Abstract
{
    public interface IAuthorService : IBaseServices<Author>
    {
        Task<Author> TGetInclude(Expression<Func<Author, bool>> expression, params Expression<Func<Author, object>>[] includes);


    }
}
