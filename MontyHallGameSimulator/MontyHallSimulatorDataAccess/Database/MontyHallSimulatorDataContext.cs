using Microsoft.EntityFrameworkCore;
using MontyHallSimulatorDataAccess.Database.Entities;

namespace MontyHallSimulatorDataAccess.Database
{
    public class MontyHallSimulatorDataContext : DbContext
    {
        public MontyHallSimulatorDataContext(DbContextOptions<MontyHallSimulatorDataContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MontyHallGame>().ToTable("MontyHallGame");
            modelBuilder.Entity<MontyHallBatch>().ToTable("MontyHallBatch");

            modelBuilder.Entity<MontyHallGame>()
                .HasOne(o => o.MontyHallBatch)
                .WithMany(o => o.MontyHallGames)
                .HasForeignKey(o => o.BatchId);

            base.OnModelCreating(modelBuilder);
        }


        public void EnableChanges()
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public DbSet<MontyHallBatch> montyHallBatches { get; set; }
        public DbSet<MontyHallGame> montyHallGames { get; set; }
    }
}
