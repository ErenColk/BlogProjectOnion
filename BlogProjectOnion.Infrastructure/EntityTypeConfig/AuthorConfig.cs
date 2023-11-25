using BlogProjectOnion.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.EntityTypeConfig
{
    public class AuthorConfig : BaseEntityConfig<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.FirstName).IsRequired(true).HasMaxLength(35).HasAnnotation("MinLength", 3); 
            builder.Property(x=> x.LastName).IsRequired(true).HasMaxLength(35).HasAnnotation("MinLength", 3);
            builder.Property(x => x.ImagePath).HasMaxLength(255).IsRequired(false);

            base.Configure(builder);
        }

    }
}
