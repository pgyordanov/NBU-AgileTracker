namespace AgileTracker.TasksService.Application.Mapping
{
    using System.Reflection;

    using AgileTracker.Common.Application.Mapping;

    public class MappingProfile: BaseMappingProfile
    {
        public MappingProfile() => this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
