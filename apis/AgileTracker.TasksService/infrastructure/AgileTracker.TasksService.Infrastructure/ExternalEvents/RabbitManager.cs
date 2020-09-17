namespace AgileTracker.TasksService.Infrastructure.ExternalEvents
{
    using System;
    using System.Text;

    using AgileTracker.Common.Events;
    using AgileTracker.TasksService.Application.Configuration;
    using AgileTracker.TasksService.Application.Contracts;

    using Microsoft.Extensions.ObjectPool;
    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;

    using RabbitMQ.Client;

    public class RabbitManager : IPublishExternalEvent
    {
        private readonly object _lockObject = new object();
        private readonly DefaultObjectPool<IModel> _objectPool;
        private readonly RabbitSettings _rabbitSettings;

        public RabbitManager(IPooledObjectPolicy<IModel> objectPolicy, IOptions<RabbitSettings> rabbitSettings)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy);
            this._rabbitSettings = rabbitSettings.Value;
        }

        public void Publish<TMessage>(TMessage message, EventType eventType) 
            where TMessage : class
        {
            var channel = _objectPool.Get();

            try
            {
                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();

                lock (this._lockObject)
                {
                    channel.BasicPublish(this._rabbitSettings.PublishExchangeName, eventType.ToString(), properties, sendBytes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}
