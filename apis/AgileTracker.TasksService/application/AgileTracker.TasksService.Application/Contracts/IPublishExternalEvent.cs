namespace AgileTracker.TasksService.Application.Contracts
{
    using AgileTracker.Common.Events;

    public interface IPublishExternalEvent
    {
        void Publish<TMessage>(TMessage message, EventType eventType)
            where TMessage : class;
    }
}
