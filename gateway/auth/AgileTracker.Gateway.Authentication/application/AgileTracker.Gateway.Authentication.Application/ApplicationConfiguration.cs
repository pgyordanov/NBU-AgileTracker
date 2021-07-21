namespace AgileTracker.Gateway.Authentication.Application
{
    using System.Reflection;

    using AgileTracker.Common.Application.Behaviours;
    using AgileTracker.Gateway.Authentication.Application.Identity.Configuration;

    using AutoMapper;

    using MediatR;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityServerSettings>
               (
                   configuration.GetSection(nameof(IdentityServerSettings)),
                   config => { config.BindNonPublicProperties = true; }
               );

            return services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        }
    }
}
