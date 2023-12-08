using Microsoft.AspNetCore.Identity;

namespace BlogProjectOnion.Presentation.Models
{
    public class AppUserIdentityValidator : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Parola en az {length}  karakter olmalıdır"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresLower",
                Description = $"Lütfen en az 1 büyük harf giriniz "
            };
        }


        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUpper",
                Description = $"Lütfen en az 1 küçük harf giriniz "
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresDigit",
                Description = $"Lütfen en az 1 tane rakam giriniz"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = $"Lütfen en az 1 tane sembol giriniz"
            };
        }


        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = "Bu kullanıcı adıyla daha önce kayıt olundu."


            };
        }


        public override IdentityError DuplicateEmail(string email)
        {

            return new IdentityError()
            {

                Code = "DuplicateEmail",
                Description = "Bu mail adresi ile daha önce kayıt olundu."
            };

        }
    }
}
