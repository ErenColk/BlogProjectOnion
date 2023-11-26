using BlogProjectOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Abstract
{
    public interface IPostService : IBaseServices<Post>
    {
        Task<List<Post>> GetIncludePost(Expression<Func<Post, bool>> expression = null, params Expression<Func<Post, object>>[] include);
        Task<Post> GetByIncludePost(Expression<Func<Post, bool>> expression = null, params Expression<Func<Post, object>>[] include);

    }
}
