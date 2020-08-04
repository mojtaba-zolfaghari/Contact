using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Entities.Config
{
   public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(contact => contact.Name).HasMaxLength(450).IsRequired();
            builder.Property(contact => contact.HomePhone).IsRequired();
            builder.Property(contact => contact.PhoneNumber).IsRequired();
        }
    }
}
