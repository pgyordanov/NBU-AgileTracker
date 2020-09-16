namespace AgileTracker.StatisticsService.Application
{
    using System.Reflection;

    using MediatR;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                        .AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
