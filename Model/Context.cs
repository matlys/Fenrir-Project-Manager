using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using Model.Models;

namespace Model
{
    public class Context : IdentityDbContext
    {
        public Context()
            : base("DefaultConnection")
        {
        }

        public static Context Create()
        {
            return new Context();
        }

        public DbSet<User> ProjectUsers { get; set; }
        public DbSet<Issue> ProjectIssues { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
