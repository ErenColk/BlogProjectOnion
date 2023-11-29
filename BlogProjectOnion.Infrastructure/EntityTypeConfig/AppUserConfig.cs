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
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(30).HasAnnotation("MinLength", 5); ;
            builder.Property(x => x.Email).IsRequired(true);
            builder.Property(x => x.ImagePath).HasMaxLength(255).IsRequired(false);

 
            base.Configure(builder);
        }

    }
}
