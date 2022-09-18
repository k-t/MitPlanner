using Microsoft.EntityFrameworkCore;
using MitPlanner.Data.Model;

namespace MitPlanner.Data
{
    internal class MitPlannerDbContext : DbContext
    {
        private readonly string dbPath;

        public MitPlannerDbContext(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public DbSet<ActionTimeline> ActionTimelines => Set<ActionTimeline>();

        public DbSet<TimelineActor> TimelineActors => Set<TimelineActor>();

        public DbSet<TimelineAction> TimelineActions => Set<TimelineAction>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={dbPath}");
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionTimeline>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.EncounterId)
                    .HasMaxLength(50)
                    .IsRequired();
                
                entity.HasMany(e => e.TimelineActors)
                    .WithOne()
                    .HasForeignKey(e => e.ActionTimelineId)
                    .IsRequired();

                entity.HasMany(e => e.TimelineActions)
                    .WithOne()
                    .HasForeignKey(e => e.ActionTimelineId)
                    .IsRequired();
            });

            modelBuilder.Entity<TimelineActor>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.JobId)
                    .HasMaxLength(3)
                    .IsRequired();

                entity.HasMany(e => e.TimelineActions)
                    .WithOne()
                    .HasForeignKey(e => e.TimelineActorId)
                    .IsRequired();
            });

            modelBuilder.Entity<TimelineAction>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.TimelineNodeId).IsRequired();

                entity.Property(e => e.ActionName)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            // Data

            modelBuilder.Entity<ActionTimeline>()
                .HasData(
                    new ActionTimeline(1, "p5s"),
                    new ActionTimeline(2, "p6s"),
                    new ActionTimeline(3, "p7s")
                );;

            modelBuilder.Entity<TimelineActor>().HasData(GenerateTimelineActors(3));
        }

        private static readonly string[] DefaultJobs = new[] { "GNB", "DRK", "WHM", "SCH", "MNK", "NIN", "RDM", "MCH" };

        private static IEnumerable<TimelineActor> GenerateTimelineActors(int timelineCount)
        {
            var id = 1;
            for (int timelineId = 1; timelineId <= timelineCount; timelineId++)
                foreach (var job in DefaultJobs)
                    yield return new TimelineActor(id++, timelineId, job);
        }
    }
}
