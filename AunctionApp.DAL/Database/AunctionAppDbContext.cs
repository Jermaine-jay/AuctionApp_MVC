using AunctionApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AunctionApp.DAL.Database
{
    public class AunctionAppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AunctionAppDbContext(DbContextOptions<AunctionAppDbContext> options) : base(options)
        {

        }

        public DbSet<Bid> Bids { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*modelBuilder.Entity<Product>()
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

                p.Property(p => p.Bidder)
                    .HasMaxLength(50)
                    .IsRequired();
            });*/


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

            modelBuilder.Entity<Product>()
                 .HasMany(u => u.BidList)
                 .WithOne(w => w.Product)
                 .HasForeignKey(w => w.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
