using BlogProjectOnion.Application.Extensions;
using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.AuthorDTOs
{
    public class UpdateAuthorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Must to type First Name!")]
        [MinLength(3, ErrorMessage = "Minimum length is 3")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Must to type Last Name!")]
        [MinLength(3, ErrorMessage = "Minimum length is 3")]
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        [PictureFileExtension] //Custom Data Annotation  Attribute yazdık. jpg, jpeg ve png uzantılı dosyaları kabul ediyor.
        public IFormFile UploadPath { get; set; }
        public DateTime UpdatedDate => DateTime.Now;
        public Status Status => Status.Modified;

    }
}
