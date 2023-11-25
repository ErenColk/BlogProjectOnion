using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs
{
    public interface IUploadImagePath
    {
        public IFormFile UploadPath { get; set; }

    }
}
