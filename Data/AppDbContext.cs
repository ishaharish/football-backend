using Microsoft.EntityFrameworkCore;
using football_backend.Models;

namespace football_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Poll> Polls { get; set; } = null!;
        public DbSet<SystemConfig> SystemConfigs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Team Name uniqueness
            modelBuilder.Entity<Team>()
                .HasIndex(t => t.TeamName)
                .IsUnique();

            // Configure Poll uniqueness (1 user can vote only once)
            modelBuilder.Entity<Poll>()
                .HasIndex(p => p.UserId)
                .IsUnique();
            
            // Seed the initial SystemConfig
            modelBuilder.Entity<SystemConfig>().HasData(
                new SystemConfig { Id = 1, IsResultPublic = false }
            );
        }
    }
}
