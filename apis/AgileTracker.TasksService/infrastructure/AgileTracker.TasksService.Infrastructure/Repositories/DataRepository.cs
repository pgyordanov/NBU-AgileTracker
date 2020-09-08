namespace AgileTracker.TasksService.Infrastructure.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Infrastructure.Persistance;

    internal class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        public DataRepository(AgileTrackerTasksDbContext dbContext)
        {
            this.Data = dbContext;
        }

        protected AgileTrackerTasksDbContext Data { get; }

        public IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Data.Update(entity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
