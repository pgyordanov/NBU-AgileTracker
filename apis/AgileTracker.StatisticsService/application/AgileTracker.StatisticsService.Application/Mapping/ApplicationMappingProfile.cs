namespace AgileTracker.StatisticsService.Application.Mapping
{
    using System.Reflection;

    using AgileTracker.Common.Application.Mapping;

    public class ApplicationMappingProfile: BaseMappingProfile
    {
        public ApplicationMappingProfile()
            => this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
