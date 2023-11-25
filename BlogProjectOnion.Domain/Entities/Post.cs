using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Entities
{
    public class Post : IBaseEntity
    {
        public Post()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        [NotMapped] // DB' de yer almayacak
        public IFormFile UploadPath { get; set; }

        //Navigation Properties
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public int Id { get ; set ; }
        public DateTime CreatedDate { get ; set ; }
        public DateTime? UpdatedDate { get ; set ; }
        public DateTime? DeletedDate { get ; set ; }
        public Status Status { get ; set ; }
    }
}
