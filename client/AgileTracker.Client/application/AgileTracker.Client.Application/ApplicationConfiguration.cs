namespace AgileTracker.Client.Application
{
    using System.Reflection;

    using AgileTracker.Client.Application.Configuration;

    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GatewaySettings>
               (
                   configuration.GetSection(nameof(GatewaySettings)),
                   options => options.BindNonPublicProperties = true
               );

            return services
                .AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
