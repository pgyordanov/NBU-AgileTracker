using Microsoft.Extensions.DependencyInjection;

namespace AgileTracker.StatisticsService.Web
{
    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
            return services;
                //.AddControllers();
        }
    }
}
