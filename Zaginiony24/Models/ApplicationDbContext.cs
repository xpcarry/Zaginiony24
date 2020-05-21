using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Zaginiony24.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Notice>()
                .HasOne(n => n.AppUser)
                .WithMany(a => a.Notices)
                .HasForeignKey(n => n.AppUserId);

            builder.Entity<Notice>()
                .Property(p => p.NoticeId)
                .ValueGeneratedOnAdd();
            builder.Entity<AppUser>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<Role>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
        public DbSet<Notice> Notices { get; set; }
    }
}
