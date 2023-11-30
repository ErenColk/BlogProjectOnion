using BlogProjectOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Abstract
{
    public interface ICommentService : IBaseServices<Comment>
    {
		Task<List<Comment>> GetCommentThenInclude(Expression<Func<Comment, bool>> expression = null);

	}
}
