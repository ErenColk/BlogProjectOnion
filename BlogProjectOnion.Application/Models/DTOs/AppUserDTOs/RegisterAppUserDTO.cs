using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.CommentDTOs
{
    public class RegisterAppUserDTO : IUploadImagePath
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        [DataType(DataType.Password)] // tipini belirtiyor
        [Compare(nameof(Password))] // iki passwordu karşılaştırıyor
        public string ConfirmPassword { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? UploadPath { get; set; }

        public DateTime CreatedDate = DateTime.Now;

        public Status Status = Status.Active;
    }
}
