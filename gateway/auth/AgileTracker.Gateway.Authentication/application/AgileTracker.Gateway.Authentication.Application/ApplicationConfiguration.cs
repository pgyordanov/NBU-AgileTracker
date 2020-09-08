namespace AgileTracker.Gateway.Authentication.Application
{
    using System.Reflection;

    using AgileTracker.Common.Application.Behaviours;

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        }
    }
}
