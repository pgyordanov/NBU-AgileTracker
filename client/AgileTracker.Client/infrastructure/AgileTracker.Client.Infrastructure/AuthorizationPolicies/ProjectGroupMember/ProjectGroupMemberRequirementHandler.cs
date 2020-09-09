namespace AgileTracker.Client.Infrastructure.AuthorizationPolicies.ProjectGroupMember
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;

    public class ProjectGroupMemberRequirementHandler : AuthorizationHandler<ProjectGroupMemberRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ProjectGroupMemberRequirementHandler(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectGroupMemberRequirement requirement)
        {
            if(context.Resource is Endpoint endpoint)
            {
                object projectGroupId;
                var res = this._contextAccessor.HttpContext.GetRouteData().Values.TryGetValue("projectGroupId", out projectGroupId);

                if (!res)
                {
                    context.Fail();
                }

                var claimType = $"projectgroup.{projectGroupId}";

                if (context.User.HasClaim(c => c.Type == claimType))
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
