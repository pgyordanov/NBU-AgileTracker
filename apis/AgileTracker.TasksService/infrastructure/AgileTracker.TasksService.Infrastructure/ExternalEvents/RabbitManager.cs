﻿namespace AgileTracker.TasksService.Infrastructure.ExternalEvents
{
    using System;
    using System.Text;

    using AgileTracker.TasksService.Application.Contracts;

    using Microsoft.Extensions.ObjectPool;

    using Newtonsoft.Json;

    using RabbitMQ.Client;

    public class RabbitManager : IPublishExternalEvent
    {
        private readonly object _lockObject = new object();
        private readonly DefaultObjectPool<IModel> _objectPool;

        public RabbitManager(IPooledObjectPolicy<IModel> objectPolicy)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy);
        }

        public void Publish<TMessage>(TMessage message, string exchangeName, string exchangeType, string routeKey) 
            where TMessage : class
        {
            var channel = _objectPool.Get();

            try
            {
                channel.ExchangeDeclare(exchangeName, exchangeType, false, true, null);

                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();

                lock (this._lockObject)
                {
                    channel.BasicPublish(exchangeName, routeKey, properties, sendBytes);
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