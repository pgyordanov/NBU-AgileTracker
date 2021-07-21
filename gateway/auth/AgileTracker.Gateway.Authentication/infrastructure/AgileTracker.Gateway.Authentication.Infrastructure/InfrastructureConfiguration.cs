namespace AgileTracker.Gateway.Authentication.Infrastructure
{
    using System.Security.Claims;

    using AgileTracker.Common.Infrastructure;
    using AgileTracker.Gateway.Authentication.Application.Identity.Contracts;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Contracts;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity.Models;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer.InitialData;
    using AgileTracker.Gateway.Authentication.Infrastructure.IdentityApi;

    using IdentityServer4;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IIdentityServerInitialData, ApiResourceData>()
                .AddTransient<IIdentityServerInitialData, ClientData>()
                .AddTransient<IIdentityServerInitialData, IdentityResourceData>()
                .AddTransient<IIdentityServerInitialData, ApiScopeData>();

            services
                .AddDbContext<AgileTrackerIdentityDbContext>
                (
                    options =>
                        options.UseSqlServer(
                            configuration.GetConnectionString("IdentityConnection"),
                            b => b.MigrationsAssembly(typeof(AgileTrackerIdentityDbContext).Assembly.FullName)
                        )
                )
                .AddTransient<IInitializer, AgileTrackerIdentityDbInitializer>()
                .AddTransient<IInitializer, AgileTrackerIdentityServerConfigurationDbInitializer>()
                .AddTransient<IInitializer, AgileTrackerIdentityServerOperationalDbInitializer>();


            services
                .AddIdentity<AgileTrackerUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 4;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AgileTrackerIdentityDbContext>()
                .AddDefaultTokenProviders();

            services
                .ConfigureApplicationCookie(config =>
                {
                    config.LoginPath = "/auth/login";
                    config.LogoutPath = "/auth/logout";
                });

            services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseInformationEvents = true;

                    options.IssuerUri = configuration["IdentityServerSettings:IssuerUri"];
                })
                .AddAspNetIdentity<AgileTrackerUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer
                    (
                        configuration.GetConnectionString("IdentityServerConnection"),
                        sqlb => sqlb.MigrationsAssembly(typeof(AgileTrackerIdentityDbInitializer).Assembly.FullName)
                    );
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer
                    (
                        configuration.GetConnectionString("IdentityServerConnection"),
                        sqlb => sqlb.MigrationsAssembly(typeof(AgileTrackerIdentityDbInitializer).Assembly.FullName)
                    );
                })
                .AddDeveloperSigningCredential();

            //adds suport for hosting a local API as a protected resource along with identity server
            services.AddLocalApiAuthentication();
            //custom authorization requirements- require manager role claim
            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            services
                .AddTransient<IIdentity, IdentityService>()
                .AddTransient<IIdentityApi, IdentityApiService>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            return app
                .UseIdentityServer()
                .UseAuthorization();
        }
    }
}
