﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Models.DTOs.CommentDTOs
{
    public class LoginAppUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool IsEmailConfirmed { get; set; }
        public bool RememberMe { get; set; }

    }
}
