using BlogProjectOnion.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.EntityTypeConfig
{
    public class PostConfig : BaseEntityConfig<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Title).IsRequired(true).HasMaxLength(150);
            builder.Property(x=> x.ImagePath).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.Content).IsRequired(true);

            base.Configure(builder);
        }
    }
}
