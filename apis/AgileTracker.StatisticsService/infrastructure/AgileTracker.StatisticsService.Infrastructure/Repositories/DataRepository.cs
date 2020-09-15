namespace AgileTracker.StatisticsService.Infrastructure.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.StatisticsService.Infrastructure.Persistance;

    internal class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class,  IAggregateRoot
    {
        public DataRepository(AgileTrackerStatisticsDbContext data)
            => this.Data = data;

        protected AgileTrackerStatisticsDbContext Data { get; private set; }

        public IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Data.Update(entity);

            await this.Data.SaveChangesAsync();
        }
    }
}
