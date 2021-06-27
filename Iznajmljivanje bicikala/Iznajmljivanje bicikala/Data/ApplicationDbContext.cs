using Iznajmljivanje_bicikala.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iznajmljivanje_bicikala.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Bicycle> Bicycles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserBicycle>()
                .HasKey(e => new { e.BicycleId, e.UserId });

            builder.Entity<UserBicycle>()
                .HasOne(e => e.User)
                .WithMany(e => e.UserBicycles)
                .HasForeignKey(e => e.UserId);

            builder.Entity<UserBicycle>()
                .HasOne(e => e.Bicycle)
                .WithMany(e => e.UserBicycles)
                .HasForeignKey(e => e.BicycleId);
        }
    }
}