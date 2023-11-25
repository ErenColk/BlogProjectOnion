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
    public class GenreManager : BaseManager<Genre>, IGenreService
    {
        private readonly IBaseRepository<Genre> _baseRepository;

        public GenreManager(IBaseRepository<Genre> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
    }
}
