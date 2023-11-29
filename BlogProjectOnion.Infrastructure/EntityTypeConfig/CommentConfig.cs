using BlogProjectOnion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.EntityTypeConfig
{
    public class CommentConfig : BaseEntityConfig<Comment>
    {

        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).IsRequired(true).HasMaxLength(255);

            builder
            .HasOne(c => c.AppUser)
            .WithMany(u => u.Comments)
            .OnDelete(DeleteBehavior.Restrict);


            builder
           .HasOne(c => c.Post)
           .WithMany(p => p.Comments)
           .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
    }
}
