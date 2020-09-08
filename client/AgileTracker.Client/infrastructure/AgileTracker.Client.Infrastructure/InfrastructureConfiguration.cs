namespace AgileTracker.Client.Infrastructure
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AgileTracker.Client.Application.Contracts;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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

                    configureOptions.Scope.Add("AgileTrackerGateway.access");
                    configureOptions.Scope.Add("IdentityServerApi.access");
                    configureOptions.Scope.Add("offline_access");

                    configureOptions.SaveTokens = true;

                    configureOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.Name 
                    };
                });

            services
                .AddHttpClient<IGatewayService, GatewayService>();

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
