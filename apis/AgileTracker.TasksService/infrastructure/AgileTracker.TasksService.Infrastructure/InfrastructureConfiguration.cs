namespace AgileTracker.TasksService.Infrastructure
{
    using System.Reflection;

    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.Common.Infrastructure;
    using AgileTracker.TasksService.Infrastructure.Persistance;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json.Serialization;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<AgileTrackerTasksDbContext>(options =>
                    options.UseSqlServer
                    (
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(AgileTrackerTasksDbContext))!.FullName)
                    )
                )
                .AddTransient<IInitializer, AgileTrackerTasksDbInitializer>();

            services
                .Scan(scan => scan
                            .FromCallingAssembly()
                            .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                            .AsMatchingInterface()
                            .WithTransientLifetime());

            return services;
        }
    }
}
