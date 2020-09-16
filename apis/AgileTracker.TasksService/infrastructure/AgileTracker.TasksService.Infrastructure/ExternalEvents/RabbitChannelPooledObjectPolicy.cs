namespace AgileTracker.TasksService.Infrastructure.ExternalEvents
{
    using AgileTracker.TasksService.Application.Configuration;

    using Microsoft.Extensions.ObjectPool;
    using Microsoft.Extensions.Options;

    using RabbitMQ.Client;

    public class RabbitChannelPooledObjectPolicy : IPooledObjectPolicy<IModel>
    {
        private readonly RabbitSettings _rabbitSettings;
        private readonly IConnection _connection;

        public RabbitChannelPooledObjectPolicy(IOptions<RabbitSettings> rabbitSettings)
        {
            this._rabbitSettings = rabbitSettings.Value;
            this._connection = this.GetConnection();
        }

        public IModel Create()
        {
            return this._connection.CreateModel();
        }

        public bool Return(IModel obj)
        {
            if (obj.IsOpen)
            {
                return true;
            }
            else
            {
                obj?.Dispose();
                return false;
            }
        }

        private IConnection GetConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = this._rabbitSettings.HostName,
                Port = int.Parse(this._rabbitSettings.Port),
                VirtualHost = this._rabbitSettings.VirtualHost,
                UserName = this._rabbitSettings.Username,
                Password = this._rabbitSettings.Password
            };

            return factory.CreateConnection();
        }
    }
}
