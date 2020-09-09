namespace AgileTracker.Client.Infrastructure.AuthorizationPolicies.ProjectGroupOwner
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ProjectGroupOwnerRequirementHandler : AuthorizationHandler<ProjectGroupOwnerRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ProjectGroupOwnerRequirementHandler(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectGroupOwnerRequirement requirement)
        {
            if (context.Resource is Endpoint endpoint)
            {
                object projectGroupId;
                var res = this._contextAccessor.HttpContext.GetRouteData().Values.TryGetValue("projectGroupId", out projectGroupId);

                if (!res)
                {
                    context.Fail();
                }
                var claimType = $"projectgroup.{projectGroupId}";

                if (context.User.HasClaim(c => c.Type == claimType && c.Value == "owner" ))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }

            return Task.CompletedTask;
        }
    }
}
