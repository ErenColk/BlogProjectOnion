using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogProjectOnion.Application.Extensions
{
    public class PictureFileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
             
           IFormFile file = value as IFormFile; // Formdan gelen dosyayı al

            if (file != null)
            {


                var extension = Path.GetExtension(file.FileName).ToLower(); // JPEG, PNG => jpeg, png

                string[] extensions = { "jpg", "jpeg", "png" };

                bool result = extensions.Any(x => x.EndsWith(extension));

                if (!result)
                {
                    return new ValidationResult("Valid format is 'jpg' , 'png' , 'jpeg' ");

                }
            }
            return ValidationResult.Success; // Validasyon başarılı

        }

    }
}
