using AunctionApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.DAL.Database
{
    public class AunctionAppDbContext : DbContext
    {
        public AunctionAppDbContext(DbContextOptions<AunctionAppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  

            modelBuilder.Entity<Product>()
                .Property(t => t.Description)
                .HasMaxLength(3000);

            modelBuilder.Entity<Product>()
                .Property(t => t.ProductName)
                .HasMaxLength(500);

            modelBuilder.Entity<Product>()
                    .Property(t => t.ActualAmount)
                    .IsRequired();

            modelBuilder.Entity<Bid>(p =>
            {
                p.Property(p => p.BidTime)
                    .HasDefaultValueSql("getdate()");

                p.Property(p => p.BidderFirstName)
                    .HasMaxLength(50)
                    .IsRequired();
                p.Property(p => p.BidderLastName)
                   .HasMaxLength(50)
                   .IsRequired();
            });

            modelBuilder.Entity<User>(e =>
            {
                e.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(p => p.LastName)
                   .IsRequired(false)
                   .HasMaxLength(50);

                e.Property(u => u.Email)
                    .HasMaxLength(100)
                    .IsRequired();

                e.Property(u => u.PhoneNumber)
                   .HasMaxLength(12)
                   .IsRequired();

                e.Property(u => u.UserName)
                   .IsRequired();

                e.HasIndex(u => u.UserName, $"IX_Unique_{nameof(User.UserName)}")
                  .IsUnique();
                  
                e.HasIndex(p => new { p.Email, p.PhoneNumber }, $"IX_Unique_{nameof(User.Email)}{nameof(User.PhoneNumber)}")
                   .IsUnique();

            });

            modelBuilder.Entity<Admin>(e =>
            {
                e.Property(p => p.AdminUserName)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(u => u.Email)
                    .HasMaxLength(100)
                    .IsRequired();
                e.Property(u => u.PhoneNumber)
                   .HasMaxLength(12)
                   .IsRequired();

                e.HasIndex(p => new { p.Email, p.PhoneNumber }, $"IX_Unique_{nameof(User.Email)}{nameof(User.PhoneNumber)}")
                   .IsUnique();

            });

            modelBuilder.Entity<Comment>(e =>
            {
                e.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(u => u.Email)
                    .HasMaxLength(200)
                    .IsRequired();

                e.Property(u => u.Subject)
                   .HasMaxLength(500)
                   .IsRequired(false);

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
