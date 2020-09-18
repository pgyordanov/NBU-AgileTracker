namespace AgileTracker.StatisticsService.Infrastructure.Listeners
{
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Events.Models;
    using AgileTracker.StatisticsService.Application.Configuration;
    using AgileTracker.StatisticsService.Application.Contracts;
    using AgileTracker.StatisticsService.Infrastructure.ExternalEvents;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.ObjectPool;
    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class TaskFinishedEventListener : RabbitEventListener
    {
        private readonly RabbitSettings _rabbitSettings;
        private readonly IServiceScopeFactory _scopeFactory;

        public TaskFinishedEventListener(IPooledObjectPolicy<IModel> objectPolicy, IOptions<RabbitSettings> rabbitSettings, IServiceScopeFactory scopeFactory) 
            : base(objectPolicy)
        {
            this._rabbitSettings = rabbitSettings.Value;
            this._scopeFactory = scopeFactory;
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

        protected override async void HandleMessage(object message)
        {
            var castMessage = message as ProjectGroupCreatedEventModel;

            using var scope = this._scopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetService<ITaskEstimationRepository>();
            await repository.AddProjectGroupOwnership(castMessage!.ProjectGroupId, castMessage.ProjectGroupOwnerId);
        }
    }
}
