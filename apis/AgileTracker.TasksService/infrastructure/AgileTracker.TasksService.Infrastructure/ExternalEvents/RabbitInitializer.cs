namespace AgileTracker.TasksService.Infrastructure.ExternalEvents
{
    using System;

    using AgileTracker.Common.Events;
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
                channel.ExchangeDeclare(this._rabbitSettings.PublishExchangeName, ExchangeType.Topic, true, false, null);

                channel.QueueDeclare(queue: this._rabbitSettings.TaskFinishedQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(this._rabbitSettings.TaskFinishedQueueName, this._rabbitSettings.PublishExchangeName, EventType.TaskFinished.ToString(), null);

                channel.QueueDeclare(queue: this._rabbitSettings.ProjectGroupCreatedQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(this._rabbitSettings.ProjectGroupCreatedQueueName, this._rabbitSettings.PublishExchangeName, EventType.ProjectGroupCreated.ToString(), null);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
