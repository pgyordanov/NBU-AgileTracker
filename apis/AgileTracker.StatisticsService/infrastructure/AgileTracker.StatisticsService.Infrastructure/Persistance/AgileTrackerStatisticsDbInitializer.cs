namespace AgileTracker.StatisticsService.Infrastructure.Persistance
{
    using AgileTracker.Common.Infrastructure;

    using Microsoft.EntityFrameworkCore;

    public class AgileTrackerStatisticsDbInitializer : IInitializer
    {
        private readonly AgileTrackerStatisticsDbContext _dbContext;

        public AgileTrackerStatisticsDbInitializer(AgileTrackerStatisticsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Initialize()
        {
            this._dbContext.Database.Migrate();
        }
    }
}
