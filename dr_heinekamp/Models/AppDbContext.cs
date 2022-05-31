using dr_heinekamp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Models
{
    // DbContext class
    // This class inherits from IdentityDbContext 
    // IdentityDbContext provides for us all tables that we need in our project for authentication and authorization based on the 
    // Asp.net core identity 
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFiles>().HasMany(b => b.SubFiles).WithOne(p => p.UserFiles)
    .HasForeignKey(p => p.UserFilesId)
    .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserFiles> UserFiles { get; set; }
        public DbSet<ShareLink_WithUsers> ShareLink_WithUsers { get; set; }
        public DbSet<SubFiles> SubFiles { get; set; }
    }
}