namespace AgileTracker.TasksService.Application.Configuration
{
    public class RabbitSettings
    {
        public string HostName { get; set; }

        public string VirtualHost { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Port { get; set; }

        public string PublishExchangeName { get; set; }

        public string ProjectGroupCreatedQueueName { get; set; }

        public string TaskFinishedQueueName { get; set; }
    }
}
