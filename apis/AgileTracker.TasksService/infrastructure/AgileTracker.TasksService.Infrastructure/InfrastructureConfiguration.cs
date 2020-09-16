namespace AgileTracker.TasksService.Infrastructure
{
    using System.Reflection;

    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.Common.Infrastructure;
    using AgileTracker.TasksService.Application.Contracts;
    using AgileTracker.TasksService.Infrastructure.ExternalEvents;
    using AgileTracker.TasksService.Infrastructure.Persistance;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.ObjectPool;

    using RabbitMQ.Client;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<AgileTrackerTasksDbContext>(options =>
                    options.UseSqlServer
                    (
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(AgileTrackerTasksDbContext))!.FullName)
                    )
                )
                .AddTransient<IInitializer, AgileTrackerTasksDbInitializer>();

            services
                .Scan(scan => scan
                            .FromCallingAssembly()
                            .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                            .AsMatchingInterface()
                            .WithTransientLifetime());

            return services;
        }

        public static IServiceCollection AddRabbit(this IServiceCollection services)
        {
            services
                .AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
                .AddSingleton<IPooledObjectPolicy<IModel>, RabbitChannelPooledObjectPolicy>()
                .AddSingleton<IPublishExternalEvent, RabbitManager>()
                .AddTransient<IInitializer, RabbitManager>();

            return services;
        }
    }
}
