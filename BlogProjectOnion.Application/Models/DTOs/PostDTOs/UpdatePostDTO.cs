using BlogProjectOnion.Application.Models.VMs;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.PostDTOs
{
    public class UpdatePostDTO : IUploadImagePath
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }
        public Status Status => Status.Modified;
        public DateTime UpdatedDate => DateTime.Now;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        // Author ve Genre VM Listeleri dolduracak. 
        public List<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
