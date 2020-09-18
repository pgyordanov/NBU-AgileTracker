namespace AgileTracker.StatisticsService.Infrastructure.ExternalEvents
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.ObjectPool;

    using RabbitMQ.Client;

    public abstract class RabbitEventListener: BackgroundService
    {
        protected readonly object _lockObject = new object();
        protected readonly DefaultObjectPool<IModel> _objectPool;

        public RabbitEventListener(IPooledObjectPolicy<IModel> objectPolicy)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy);
        }

        protected abstract void HandleMessage(object message);
    }
}
