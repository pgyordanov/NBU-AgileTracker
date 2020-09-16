namespace AgileTracker.TasksService.Application.Contracts
{
    public interface IPublishExternalEvent
    {
        void Publish<TMessage>(TMessage message, string exchangeName, string exchangeType, string routeKey)
            where TMessage : class;
    }
}
