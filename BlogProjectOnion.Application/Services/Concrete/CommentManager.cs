using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Concrete
{
    public class CommentManager : BaseManager<Comment>, ICommentService
    {
        private readonly IBaseRepository<Comment> _baseRepository;
        public CommentManager(IBaseRepository<Comment> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
    }
}
