namespace AgileTracker.Client.Infrastructure.AuthorizationPolicies.ProjectGroupMember
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;

    public class ProjectGroupMemberRequirementHandler : AuthorizationHandler<ProjectGroupMemberRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ProjectGroupMemberRequirementHandler(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectGroupMemberRequirement requirement)
        {
            string projectGroupId = context.GetResource<int>("projectGroupId", this._contextAccessor).ToString();

            if (!context.HasFailed)
            {
                var claimType = $"projectgroup.{projectGroupId}";

                context.ValidateRequirementWithClaim(requirement, claimType);
            }

            return Task.CompletedTask;
        }
    }
}
