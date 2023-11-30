using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using System.Linq.Expressions;

namespace BlogProjectOnion.Application.Services.Concrete
{
	public class CommentManager : BaseManager<Comment>, ICommentService
    {
        private readonly IBaseRepository<Comment> _baseRepository;
		private readonly ICommentRepository _commentService;

		public CommentManager(IBaseRepository<Comment> baseRepository,ICommentRepository commentService) : base(baseRepository)
        {
            _baseRepository = baseRepository;
			_commentService = commentService;
		}

		public Task<List<Comment>> GetCommentThenInclude(Expression<Func<Comment, bool>> expression = null)
		{
			return _commentService.GetCommentThenInclude(expression);
		}
	}
}
