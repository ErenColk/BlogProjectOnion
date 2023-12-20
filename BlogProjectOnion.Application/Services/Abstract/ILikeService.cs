using BlogProjectOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Abstract
{
    public interface ILikeService : IBaseServices<Like>
    {
        Task<List<Like>> TGetByInclude(Expression<Func<Like, bool>> expression = null, params Expression<Func<Like, object>>[]? includes);


    }
}
