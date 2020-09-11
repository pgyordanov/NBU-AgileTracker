namespace AgileTracker.Client.Startup
{
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Client.Infrastructure.Contracts;
    using AgileTracker.Client.Startup.Infrastructure;
    using AgileTracker.Client.Startup.Services;

    using AutoMapper;

    using IdentityModel.Client;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddScoped<ICurrentUser, CurrentUserService>()
                .AddScoped<ITokenService, TokenService>();

            services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddControllersWithViews();

            return services;
        }

        public static IApplicationBuilder UseWebComponents(this IApplicationBuilder app, IConfiguration configuration)
        {
            var authority = (string)configuration.GetValue(typeof(string), "AuthorizationServer");
            var clientId = "agiletracker_web";
            var clientSecret = "agiletracker_web";
            var cookieScheme = "AgileTrackerClientCookie";

            return
                app.UseMiddleware<RefreshExpiredTokensMiddleware>(authority, clientId, clientSecret, cookieScheme);
        }
    }
}
