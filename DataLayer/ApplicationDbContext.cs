using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace DataLayer
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new Entities.Config.ContactConfig());
            builder.ApplyConfiguration(new Entities.Config.UserMap());
            builder.ApplyConfiguration(new Entities.Config.User.UserProfileMap());
        }
        public DbSet<Entities.Contact> Contacts { get; set; }
        public  DbSet<Entities.User> Users { get; set; }
        public  DbSet<Entities.UserProfile> UserProfiles { get; set; }



        private IDbContextTransaction _transaction;

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
