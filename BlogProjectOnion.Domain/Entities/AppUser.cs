using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>  , IBaseEntity
    {
        public AppUser()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>(); 
        }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
        public string  ImagePath { get; set; }
        [NotMapped] // DB' de yer almayacak
        public IFormFile UploadPath { get; set; }

        //Navigation Properties
        public Author? Author { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
    }
}
