namespace AgileTracker.TasksService.Infrastructure.Persistance
{
    using AgileTracker.Common.Infrastructure;

    using Microsoft.EntityFrameworkCore;

    public class AgileTrackerTasksDbInitializer : IInitializer
    {
        private readonly AgileTrackerTasksDbContext _db;

        public AgileTrackerTasksDbInitializer(AgileTrackerTasksDbContext db)
        {
            this._db = db;
        }

        public void Initialize()
        {
            this._db.Database.Migrate();
        }
    }
}
