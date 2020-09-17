namespace AgileTracker.TasksService.Infrastructure.ExternalEvents
{
    using System;

    using AgileTracker.Common.Infrastructure;
    using AgileTracker.TasksService.Application.Configuration;

    using Microsoft.Extensions.ObjectPool;
    using Microsoft.Extensions.Options;

    using RabbitMQ.Client;

    public class RabbitInitializer : IInitializer
    {
        private readonly DefaultObjectPool<IModel> _defaultObjectPool;
        private readonly RabbitSettings _rabbitSettings;

        public RabbitInitializer(IPooledObjectPolicy<IModel> rabbitChannelPooledObjectPolicy, IOptions<RabbitSettings> rabbitSettings)
        {
            this._defaultObjectPool = new DefaultObjectPool<IModel>(rabbitChannelPooledObjectPolicy);
            this._rabbitSettings = rabbitSettings.Value;
        }

        public void Initialize()
        {
            var channel = this._defaultObjectPool.Get();
            try
            {
                channel.ExchangeDeclare(this._rabbitSettings.PublishExchangeName, ExchangeType.Topic, false, true, null);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
