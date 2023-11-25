using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Concrete
{
    public class AuthorManager : BaseManager<Author>, IAuthorService
    {
        private readonly IBaseRepository<Author> _baseRepository;

        public AuthorManager(IBaseRepository<Author> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
    }
}
