namespace AgileTracker.TasksService.Application
{
    using System.Reflection;

    using AgileTracker.TasksService.Application.Configuration;
    using AgileTracker.TasksService.Application.Contracts;

    using AutoMapper;

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
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
