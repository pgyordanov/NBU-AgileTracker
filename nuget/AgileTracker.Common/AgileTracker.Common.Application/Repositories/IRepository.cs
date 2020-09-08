namespace AgileTracker.Common.Application.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using AgileTracker.Domain.Common.Models;

    public interface IRepository<in TEntity>
        where TEntity: IAggregateRoot
    {
        Task Save(TEntity entity, CancellationToken cancellationToken = default);
    }
}
