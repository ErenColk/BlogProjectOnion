﻿using BlogProjectOnion.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Domain.Entities
{
    public class AppRole : IdentityRole<Guid> ,IBaseEntity
    {
        public DateTime CreatedDate { get ; set ; }
        public DateTime? UpdatedDate { get ; set ; }
        public DateTime? DeletedDate { get ; set ; }
        public Status Status { get ; set ; }


    }
}
