namespace AgileTracker.Client.Infrastructure.AuthorizationPolicies
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public static class AuthorizationHandlerContextExtensions
    {
        public static object GetResource<TData>(
            this AuthorizationHandlerContext context,
            string resourceName,
            IHttpContextAccessor contextAccessor,
            bool failOnError = true)
        {
            object resource = default!;

            switch (context.Resource)
            {
                case Endpoint _:
                    object resourceObj;
                    var res = contextAccessor.HttpContext.GetRouteData().Values.TryGetValue(resourceName, out resourceObj);

                    if (!res && failOnError)
                    {
                        context.Fail();
                    }

                    resource = resourceObj;
                    break;
                case TData passedResource:
                    resource = passedResource;
                    break;
                default:
                    if (failOnError)
                    {
                        context.Fail();
                    }
                    break;
            }

            return resource;
        }

        public static void ValidateRequirementWithClaim(this AuthorizationHandlerContext context, IAuthorizationRequirement requirement, string claimType)
        {
            if (context.User.HasClaim(c => c.Type == claimType))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }

        public static void ValidateRequirementWithClaim(this AuthorizationHandlerContext context, IAuthorizationRequirement requirement, string claimType, string claimValue)
        {
            if (context.User.HasClaim(c => c.Type == claimType && c.Value == claimValue))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
