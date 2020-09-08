namespace AgileTracker.Common.Domain.Factories
{
    using AgileTracker.Domain.Common.Models;

    public interface IFactory<out TEntity>
        where TEntity: IAggregateRoot
    {
        TEntity Build();
    }
}
