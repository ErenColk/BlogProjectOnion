using BlogProjectOnion.Application.Models.DTOs.CommentDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.ValidationRules.AppUserValidationRules
{
    public class AppUserRegisterValidator : AbstractValidator<RegisterAppUserDTO>
    {
        public AppUserRegisterValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı alanı boş geçilemez");
            RuleFor(x => x.UserName).MaximumLength(20).WithMessage("Lütfen en fazla 20 karakter girişi yapın");
            RuleFor(x => x.UserName).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapın");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş geçilemez");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrar alanı boş geçilemez");
            RuleFor(x=>x.ConfirmPassword).Equal(y=>y.Password).WithMessage("Parolalarınız eşleşmiyor");
            RuleFor(x => x.Role).NotEqual(0).WithMessage("Lütfen rol seçimi yapınız");

        }


    }
}
