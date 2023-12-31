﻿using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.VMs.PostVMs
{
    public class PostDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string SubTitle { get; set; }
        public string ImagePath { get; set; }
        public int ClickCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
    }
}
