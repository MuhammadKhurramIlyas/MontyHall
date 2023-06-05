using Microsoft.EntityFrameworkCore;
using MontyHallDataAccess.Database.Entities;

namespace MontyHallDataAccess.Database
{
    public class MontyHallContext : DbContext
    {
        public MontyHallContext(DbContextOptions<MontyHallContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MontyHallGame>().ToTable("MontyHallGame");

            base.OnModelCreating(modelBuilder);
        }

        public void EnableChanges()
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public DbSet<MontyHallGame> MontyHallGames { get; set; }
    }
}
