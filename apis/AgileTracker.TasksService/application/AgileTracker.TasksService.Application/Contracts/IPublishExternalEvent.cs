namespace AgileTracker.TasksService.Application.Contracts
{
    public interface IPublishExternalEvent
    {
        void Publish<TMessage>(TMessage message)
            where TMessage : class;
    }
}
