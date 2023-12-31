using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.FollowDtos
{
    public class CreateFollowDtos
    {
        public Guid AppUserId { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }


    }
}
