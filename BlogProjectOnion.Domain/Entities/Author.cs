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
    public class Author : IBaseEntity
    {
        public Author()
        {
            Posts = new List<Post>();
            Follows = new List<Follow>();
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
        public string?  FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped] // DB' de yer almayacak
        public IFormFile UploadPath { get; set; }

        //Navigation Properties
        public List<Post> Posts { get; set; }
        public List<Follow> Follows { get; set; }
    }
}
