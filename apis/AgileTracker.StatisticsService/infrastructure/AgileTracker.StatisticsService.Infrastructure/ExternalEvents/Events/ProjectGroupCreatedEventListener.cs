namespace AgileTracker.StatisticsService.Infrastructure.ExternalEvents
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;

    public class ProjectGroupCreatedEventListener : BackgroundService
    {
        public ProjectGroupCreatedEventListener()
        {

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
