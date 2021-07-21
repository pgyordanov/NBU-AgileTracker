namespace AgileTracker.Client.Infrastructure
{
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Client.Infrastructure.AuthenticationEvents;
    using AgileTracker.Client.Infrastructure.AuthorizationPolicies.ProjectGroupMember;
    using AgileTracker.Client.Infrastructure.AuthorizationPolicies.ProjectGroupOwner;
    using AgileTracker.Client.Infrastructure.Contracts;

    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = "AgileTrackerClientCookie";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("AgileTrackerClientCookie")
                .AddOpenIdConnect("oidc", configureOptions =>
                {
                    configureOptions.Authority = (string)configuration.GetValue(typeof(string), "AuthorizationServer");

                    configureOptions.ClientId = "agiletracker_web";
                    configureOptions.ClientSecret = "agiletracker_web";

                    configureOptions.ResponseType = "code";

                    configureOptions.Scope.Add("AgileTrackerGateway");
                    configureOptions.Scope.Add("IdentityServerApi");
                    configureOptions.Scope.Add("offline_access");

                    configureOptions.SaveTokens = true;
                    configureOptions.GetClaimsFromUserInfoEndpoint = true;

                    configureOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.Name
                    };

                    configureOptions.Events = new OpenIdConnectEvents
                    {
                        OnUserInformationReceived = AgileTrackerOpenIdConnectEvents.OnUserInformationReceived
                    };

                    //only for dev
                    configureOptions.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsProjectGroupMember", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new ProjectGroupMemberRequirement());
                });

                options.AddPolicy("IsProjectGroupOwner", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new ProjectGroupOwnerRequirement());
                });
            });

            services
                .AddTransient<IAuthorizationHandler, ProjectGroupMemberRequirementHandler>()
                .AddTransient<IAuthorizationHandler, ProjectGroupOwnerRequirementHandler>();


            services
                .AddHttpClient<IGatewayService, GatewayService>();

            services
                .AddHttpClient<IClaimsGatewayService, ClaimsGatewayService>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
