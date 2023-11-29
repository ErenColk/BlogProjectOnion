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
    public class LikeConfig : BaseEntityConfig<Like>
    {

        public override void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(x => x.Id);


            builder
            .HasOne(l => l.AppUser)
            .WithMany(u => u.Likes)
            .OnDelete(DeleteBehavior.Restrict);

            builder
           .HasOne(l => l.Post)
           .WithMany(p => p.Likes)
           .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
    }
}
