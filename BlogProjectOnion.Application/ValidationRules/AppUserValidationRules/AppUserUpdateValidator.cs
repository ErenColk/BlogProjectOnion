using BlogProjectOnion.Application.Models.VMs.AppUserVMs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.ValidationRules.AppUserValidationRules
{
    public class AppUserUpdateValidator :AbstractValidator<AppUserVM>
    {

        public AppUserUpdateValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı alanı boş geçilemez");
            RuleFor(x => x.UserName).MaximumLength(20).WithMessage("Kullanıcı adı için lütfen en fazla 20 karakter girişi yapın");
            RuleFor(x => x.UserName).MinimumLength(3).WithMessage("Kullanıcı adı için lütfen en az 3 karakter girişi yapın");

            RuleFor(x => x.FirstName).MaximumLength(20).WithMessage("Adınız için lütfen en fazla 20 karakter girişi yapın");
            RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Adınız için lütfen en az 3 karakter girişi yapın");

            RuleFor(x => x.LastName).MaximumLength(20).WithMessage("Soyadınız için lütfen en fazla 20 karakter girişi yapın");
            RuleFor(x => x.LastName).MinimumLength(3).WithMessage("Soyadınız için lütfen en az 3 karakter girişi yapın");
        }

    }
}
