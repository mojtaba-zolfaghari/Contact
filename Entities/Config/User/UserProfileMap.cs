using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Config.User
{
   public class UserProfileMap : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.LastName).IsRequired();
            builder.Property(c => c.Phone).IsRequired(false);
            builder.Property(c => c.Address);
        }
    }
}
