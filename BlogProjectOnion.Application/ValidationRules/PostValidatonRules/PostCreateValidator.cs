using BlogProjectOnion.Application.Models.VMs.PostVMs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.ValidationRules.PostValidatonRules
{
    public class PostCreateValidator : AbstractValidator<CreatePostVM>
    {

        public PostCreateValidator()
        {

            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanını boş bırakamazsınız!");
            RuleFor(x => x.Content).MinimumLength(50).WithMessage("İçerik alanına en az 50 karakter girmelisiniz!");

            RuleFor(x => x.GenreId).NotEqual(0).WithMessage("Kategori seçimi yapmalısınız");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanını boş bırakamazsınız!");
            RuleFor(x => x.Title).MinimumLength(2).WithMessage("Başlık alanı en az 2 karakter olmalı");
            RuleFor(x => x.Title).MaximumLength(250).WithMessage("Başlık alanı fazla 250 karakter olmalı");

            
            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("Alt Başlık alanını boş bırakamazsınız!");
            RuleFor(x => x.SubTitle).MinimumLength(2).WithMessage("Alt Başlık alanı en az 2 karakter olmalı");
            RuleFor(x => x.SubTitle).MaximumLength(250).WithMessage("Alt Başlık alanı fazla 250 karakter olmalı");
            
        }

    }
}
