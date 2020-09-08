namespace AgileTracker.Common.Domain.Services
{
    using AgileTracker.Domain.Common.Models;

    public interface IDomainService<out TEntity>
        where TEntity: IAggregateRoot
    {
    }
}
