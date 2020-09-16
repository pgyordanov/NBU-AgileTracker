namespace AgileTracker.StatisticsService.Infrastructure.ExternalEvents
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.StatisticsService.Infrastructure.ExternalEvents.Events.Models;

    using Microsoft.Extensions.ObjectPool;

    using Newtonsoft.Json;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class ProjectGroupCreatedEventListener : RabbitEventListener
    {
        public ProjectGroupCreatedEventListener(IPooledObjectPolicy<IModel> objectPolicy) 
            : base(objectPolicy)
        {
            var channel = this._objectPool.Get();

            channel.QueueDeclare(queue: "statsrvcQueue", durable: false, exclusive: false, autoDelete: true, arguments: null);
            channel.QueueBind("statsrvcQueue", "tasksrvcPubExchange", "", null);
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var channel = this._objectPool.Get();
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var eventModel = JsonConvert.DeserializeObject<ProjectGroupCreatedEventMessage>(content);

                HandleMessage(eventModel);

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume("statsrvcQueue", false, consumer);

            return Task.CompletedTask;
        }

        protected override void HandleMessage<ProjectGroupCreatedEventMessage>(ProjectGroupCreatedEventMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
