﻿using BlogProjectOnion.Domain.Enums;
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
            Follows = new List<Follow>();
        }


        public string? FirstName { get; set; }
        public string? LastName  { get; set; }
        public string? Address { get; set; }
        public string? About { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }   
        public Status Status { get; set; }
        public int? ConfirmCode { get; set; }
        public string  ImagePath { get; set; }
        [NotMapped] // DB' de yer almayacak
        public IFormFile UploadPath { get; set; }

        //Navigation Properties
        public Author? Author { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public List<Follow> Follows { get; set; }

    }
}
