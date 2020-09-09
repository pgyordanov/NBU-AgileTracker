namespace AgileTracker.Gateway.Authentication.Startup
{
    using System.Reflection;

    using AgileTracker.Gateway.Authentication.Application;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Contracts;
    using AgileTracker.Gateway.Authentication.Startup.Services;

    using FluentValidation.AspNetCore;

    using Microsoft.Extensions.DependencyInjection;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
            services
                .AddControllersWithViews()
                .AddFluentValidation(val =>
                        val.RegisterValidatorsFromAssemblies(new[]
                        {
                            Assembly.GetAssembly(typeof(ApplicationConfiguration)),
                            Assembly.GetExecutingAssembly()
                        }
                ))
                .AddNewtonsoftJson();

            services.AddScoped<ICurrentUser, CurrentUserService>();

            return services;
        }
    }
}
