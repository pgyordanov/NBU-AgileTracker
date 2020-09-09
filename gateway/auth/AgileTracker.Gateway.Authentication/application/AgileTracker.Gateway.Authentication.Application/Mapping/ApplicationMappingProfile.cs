namespace AgileTracker.Gateway.Authentication.Application.Mapping
{
    using System.Reflection;

    using AgileTracker.Common.Application.Mapping;

    class ApplicationMappingProfile: BaseMappingProfile
    {
        public ApplicationMappingProfile()
            => this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
