namespace AgileTracker.StatisticsService.Application
{
    using System.Reflection;

    using AgileTracker.StatisticsService.Application.Configuration;

    using MediatR;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitSettings>
                (
                    configuration.GetSection(nameof(RabbitSettings)),
                    config => { config.BindNonPublicProperties = true; }
                );

            services
                .AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
