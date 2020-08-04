using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Config
{
   public class UserMap : IEntityTypeConfiguration<Entities.User>
    {

        public void Configure(EntityTypeBuilder<Entities.User> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.Password).IsRequired();
            builder.HasOne(t => t.UserProfile).WithOne(u => u.User).HasForeignKey<UserProfile>(x => x.Id);
        }
    }
}
