﻿namespace AgileTracker.TasksService.Domain
{
    using AgileTracker.Common.Domain.Builders;
    using AgileTracker.Common.Domain.Factories;

    using Microsoft.Extensions.DependencyInjection;

    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services
                .Scan(scan => scan
                        .FromCallingAssembly()
                        .AddClasses(action => action.AssignableTo(typeof(IFactory<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                      )
                .Scan(scan => scan
                        .FromCallingAssembly()
                        .AddClasses(action => action.AssignableTo(typeof(IBuilder<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                      );
        }
    }
}
