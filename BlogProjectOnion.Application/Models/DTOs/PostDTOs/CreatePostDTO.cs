using BlogProjectOnion.Application.Models.VMs;
using BlogProjectOnion.Application.Models.VMs.AuthorVMs;
using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.PostDTOs
{
    public class CreatePostDTO : IUploadImagePath
    {
        [Required(ErrorMessage = "Must to type Title!")]
        [MinLength(10, ErrorMessage = "Minimum length is 10")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must to type Content")]
        [MinLength(3, ErrorMessage = "Minimum length is 3")]
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }
        public Status Status => Status.Active;
        public DateTime CreatedDate => DateTime.Now;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        // Author ve Genre VM Listeleri dolduracak. 

        public List<AuthorVM> Authors { get; set; }
        public List<GenreVM> Genres { get; set; }

    }
}
