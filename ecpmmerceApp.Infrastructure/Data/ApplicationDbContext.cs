﻿using ecpmmerceApp.Domain.Entities;
using ecpmmerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ecpmmerceApp.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ArchivePayment> ArchivePayments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PaymentMethod>().HasData(
               new PaymentMethod
               {
                   Id = Guid.NewGuid(),
                   Name = "Credit-Card",
               }
            );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
                
            },
             new IdentityRole
             {
                 Id = Guid.NewGuid().ToString(),
                 Name = "User",
                 NormalizedName = "USER"
             }
             );
            modelBuilder.Entity<Category>().HasData(
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Electronics"
                    },
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Furniture"
                    },
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Clothing"
                    }
                );

        }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
}
    }
}
