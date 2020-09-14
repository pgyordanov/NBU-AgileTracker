namespace AgileTracker.StatisticsService.Domain
{
    using AgileTracker.Common.Domain.Factories;

    using Microsoft.Extensions.DependencyInjection;

    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services
                .Scan(scan => scan
                            .FromCallingAssembly()
                            .AddClasses(c => c.AssignableTo(typeof(IFactory<>)))
                            .AsMatchingInterface()
                            .WithTransientLifetime()
                     );
        }
    }
}
