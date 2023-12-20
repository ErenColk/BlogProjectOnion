using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.VMs.AppUserVMs
{
    public class AppUserVM
    {
        public AppUserVM()
        {
            Posts = new List<Post>();
            Likes = new List<Like>();
        }
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }
        public DateTime CreatedDate{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string About { get; set; }
        public List<Post> Posts{ get; set; }
        public List<Like> Likes { get; set; }
        public Author Author { get; set; }


    }
}
