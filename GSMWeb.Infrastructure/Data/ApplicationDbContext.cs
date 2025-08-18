using GSMWeb.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GSMWeb.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PrivacyPolicy> PrivacyPolicies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompanyProfile>(entity =>
            {
                entity.Property(p => p.CompanyName).HasMaxLength(255);
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.Property(p => p.HoAddress).HasMaxLength(500);
                entity.Property(p => p.Address1).HasMaxLength(500);
                entity.Property(p => p.Address2).HasMaxLength(500);
                entity.Property(p => p.AboutUs).HasMaxLength(1000);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(p => p.LocationName).HasMaxLength(255);
                entity.Property(p => p.Address).HasMaxLength(1000);
                entity.Property(p => p.Remark).HasMaxLength(1000);
            });

            modelBuilder.Entity<PrivacyPolicy>(entity =>
            {
                entity.Property(p => p.PolicyName1).HasMaxLength(500);
                entity.Property(p => p.Description1).HasMaxLength(1000);

                entity.Property(p => p.PolicyName2).HasMaxLength(500);
                entity.Property(p => p.Description2).HasMaxLength(1000);

                entity.Property(p => p.PolicyName3).HasMaxLength(500);
                entity.Property(p => p.Description3).HasMaxLength(1000);

                entity.Property(p => p.PolicyName4).HasMaxLength(500);
                entity.Property(p => p.Description4).HasMaxLength(1000);

                entity.Property(p => p.PolicyName5).HasMaxLength(500);
                entity.Property(p => p.Description5).HasMaxLength(1000);
            });
        }
    }
}