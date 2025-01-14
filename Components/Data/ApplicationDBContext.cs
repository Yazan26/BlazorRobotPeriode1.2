using Microsoft.EntityFrameworkCore;

namespace DependencyInjectionExample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Distance> Distances { get; set; }
        public DbSet<ObjectCount> ObjectCounts { get; set; }
        public DbSet<BatteryPercentage> BatteryPercentages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example of Fluent API configuration
            modelBuilder.Entity<Distance>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary Key
                entity.Property(e => e.Value).IsRequired();
                entity.Property(e => e.Timestamp).IsRequired();
            });

            modelBuilder.Entity<ObjectCount>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary Key
                entity.Property(e => e.Count).IsRequired();
                entity.Property(e => e.Timestamp).IsRequired();
            });

            modelBuilder.Entity<BatteryPercentage>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary Key
                entity.Property(e => e.Percentage).IsRequired();
                entity.Property(e => e.Timestamp).IsRequired();
            });
        }
    }
}