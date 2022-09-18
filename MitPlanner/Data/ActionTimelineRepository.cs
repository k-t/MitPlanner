using Microsoft.EntityFrameworkCore;
using MitPlanner.Data.Model;

namespace MitPlanner.Data
{
    public sealed class ActionTimelineRepository : IDisposable
    {
        private const string DatabaseFileName = "data.db";

        private readonly string databasePath;

        public ActionTimelineRepository()
        {
            databasePath = Path.Combine(GetAppFolderPath(), DatabaseFileName);

            using (var db = new MitPlannerDbContext(databasePath))
            {
                db.Database.EnsureCreated();
                db.SaveChanges();
            }
        }

        public IReadOnlyCollection<ActionTimeline> GetTimelines()
        {
            using (var db = new MitPlannerDbContext(databasePath))
            {
                return db.ActionTimelines
                    .Include(t => t.TimelineActors)
                    .ThenInclude(a => a.TimelineActions)
                    .ToArray();
            }
        }

        public async Task AddTimelineActorActionAsync(TimelineAction action)
        {
            using (var db = new MitPlannerDbContext(databasePath))
            {
                db.TimelineActions.Add(action);
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveTimelineActorActionAsync(TimelineAction action)
        {
            using (var db = new MitPlannerDbContext(databasePath))
            {
                var entity = await db.TimelineActions
                    .FirstOrDefaultAsync(e =>
                        e.TimelineNodeId == action.TimelineNodeId &&
                        e.TimelineActorId == action.TimelineActorId &&
                        e.ActionName == action.ActionName);

                if (entity != null)
                {
                    db.TimelineActions.Remove(entity);
                    await db.SaveChangesAsync();
                }
            }
        }

        private static string GetAppFolderPath()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appFolderPath = Path.Combine(appDataPath, ".mitplanner");

            if (!Directory.Exists(appFolderPath))
                Directory.CreateDirectory(appFolderPath);

            return appFolderPath;
        }

        public void Dispose()
        {
        }
    }
}
