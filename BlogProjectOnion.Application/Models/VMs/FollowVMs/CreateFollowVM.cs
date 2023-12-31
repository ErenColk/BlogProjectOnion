using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.VMs.FollowVMs
{
    public class CreateFollowVM
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }

    }
}
