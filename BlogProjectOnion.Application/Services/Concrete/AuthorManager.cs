using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Concrete
{
    public class AuthorManager : BaseManager<Author>, IAuthorService
    {
        private readonly IBaseRepository<Author> _baseRepository;
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IBaseRepository<Author> baseRepository,IAuthorRepository authorRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _authorRepository = authorRepository;
        }

        public Task<Author> TGetInclude(Expression<Func<Author, bool>> expression, params Expression<Func<Author, object>>[] includes)
        {
            return _authorRepository.GetInclude(expression, includes);
        }
    }
}
