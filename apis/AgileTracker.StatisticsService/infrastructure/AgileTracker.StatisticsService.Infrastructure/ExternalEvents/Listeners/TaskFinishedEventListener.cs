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
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var channel = this._objectPool.Get();
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var eventModel = JsonConvert.DeserializeObject<TaskFinishedEventModel>(content);

                HandleMessage(eventModel);

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(this._rabbitSettings.TaskFinishedQueueName, false, consumer);

            return Task.CompletedTask;
        }

        protected override void HandleMessage<TaskFinishedEventModel>(TaskFinishedEventModel message)
        {
            throw new NotImplementedException();
        }
    }
}
