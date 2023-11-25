﻿using BlogProjectOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.CommentDTOs
{
    public class UpdateCommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Guid AppUserId { get; set; }
        public int PostId { get; set; }

        public DateTime CreatedDate => DateTime.Now;

        public Status Status = Status.Active;
    }
}
