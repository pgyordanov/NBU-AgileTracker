namespace AgileTracker.Client.Startup.Mapping
{
    using System.Reflection;
    using AgileTracker.Common.Application.Mapping;

    public class WebMappingProfile: BaseMappingProfile
    {
        public WebMappingProfile() => this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
