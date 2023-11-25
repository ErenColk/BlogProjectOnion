﻿using BlogProjectOnion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.EntityTypeConfig
{
    public abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {


            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.UpdatedDate).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(true);

        }
    }


}
