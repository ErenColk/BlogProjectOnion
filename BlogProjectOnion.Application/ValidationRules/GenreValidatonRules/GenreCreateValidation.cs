using BlogProjectOnion.Application.Models.DTOs.GenreDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.ValidationRules.GenreValidatonRules
{
    public class GenreCreateValidation : AbstractValidator<CreateGenreDTO>
    {

        public GenreCreateValidation()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Kategori adını girmelisiniz");
            RuleFor(x=>x.Name).MinimumLength(2).WithMessage("Kategori alanı en az 2 karakter olmalı");
        }
    }
}
