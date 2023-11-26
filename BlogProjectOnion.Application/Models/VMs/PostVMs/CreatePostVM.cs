using BlogProjectOnion.Application.Models.DTOs;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.VMs.PostVMs
{
    public class CreatePostVM : IUploadImagePath
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
        public int AuthorId{ get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public List<Genre> Genres { get; set; }


    }
}
