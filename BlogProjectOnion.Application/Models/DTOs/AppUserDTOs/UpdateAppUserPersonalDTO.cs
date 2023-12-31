using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.AppUserDTOs
{
    public class UpdateAppUserPersonalDTO : IUploadImagePath
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        //public string ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }

        public DateTime UpdatedDate = DateTime.Now;

        public Status Status = Status.Modified;
    }
}
