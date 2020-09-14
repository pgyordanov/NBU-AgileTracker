namespace AgileTracker.Client.Infrastructure.AuthorizationPolicies.ProjectGroupOwner
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;

    public class ProjectGroupOwnerRequirementHandler : AuthorizationHandler<ProjectGroupOwnerRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ProjectGroupOwnerRequirementHandler(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectGroupOwnerRequirement requirement)
        {
            string projectGroupId = context.GetResource<int>("projectGroupId", this._contextAccessor).ToString()!;

            if (!context.HasFailed)
            {
                var claimType = $"projectgroup.{projectGroupId}";

                context.ValidateRequirementWithClaim(requirement, claimType, "owner");
            }

            return Task.CompletedTask;
        }
    }
}
