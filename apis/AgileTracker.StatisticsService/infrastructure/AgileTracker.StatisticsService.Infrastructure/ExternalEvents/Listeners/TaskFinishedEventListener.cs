namespace AgileTracker.StatisticsService.Infrastructure.Listeners
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Events.Models;
    using AgileTracker.StatisticsService.Application.Configuration;
    using AgileTracker.StatisticsService.Infrastructure.ExternalEvents;

    using Microsoft.Extensions.ObjectPool;
    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class ProjectGroupCreatedEventListener : RabbitEventListener
    {
        private readonly RabbitSettings _rabbitSettings;

        public ProjectGroupCreatedEventListener(IPooledObjectPolicy<IModel> objectPolicy, IOptions<RabbitSettings> rabbitSettings) 
            : base(objectPolicy)
        {
            this._rabbitSettings = rabbitSettings.Value;

            var channel = this._objectPool.Get();

            channel.QueueDeclare(queue: this._rabbitSettings.ProjectGroupCreatedQueueName, durable: false, exclusive: false, autoDelete: true, arguments: null);
            channel.QueueBind(this._rabbitSettings.ProjectGroupCreatedQueueName, this._rabbitSettings.PublishExchangeName, "", null);
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var channel = this._objectPool.Get();
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var eventModel = JsonConvert.DeserializeObject<ProjectGroupCreatedEventModel>(content);

                HandleMessage(eventModel);

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(this._rabbitSettings.ProjectGroupCreatedQueueName, false, consumer);

            return Task.CompletedTask;
        }

        protected override void HandleMessage<ProjectGroupCreatedEventMessage>(ProjectGroupCreatedEventMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
