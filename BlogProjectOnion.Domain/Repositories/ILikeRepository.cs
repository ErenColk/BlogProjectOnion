using BlogProjectOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Repositories
{
    public interface ILikeRepository : IBaseRepository<Like>
    {

        Task<List<Like>> GetByInclude(Expression<Func<Like,bool>> expression = null,params Expression<Func<Like,object>>[]? includes );
    }
}
