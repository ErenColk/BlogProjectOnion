﻿using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre> , IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
