namespace AgileTracker.StatisticsService.Infrastructure
{
    using System.Reflection;

    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.Common.Infrastructure;
    using AgileTracker.StatisticsService.Infrastructure.ExternalEvents;
    using AgileTracker.StatisticsService.Infrastructure.Persistance;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.ObjectPool;

    using RabbitMQ.Client;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AgileTrackerStatisticsDbContext>(options =>
            {
                options.UseSqlServer
                (
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(AgileTrackerStatisticsDbContext))!.FullName)
                );
            })
            .AddTransient<IInitializer, AgileTrackerStatisticsDbInitializer>();

            services.Scan(scan => scan.FromCallingAssembly()
                                    .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                                    .AsMatchingInterface()
                                    .WithTransientLifetime());

            return services;
        }

        public static IServiceCollection AddRabbit(this IServiceCollection services)
        {
            services
                .AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
                .AddSingleton<IPooledObjectPolicy<IModel>, RabbitChannelPooledObjectPolicy>();

            return services;
        }
    }
}
