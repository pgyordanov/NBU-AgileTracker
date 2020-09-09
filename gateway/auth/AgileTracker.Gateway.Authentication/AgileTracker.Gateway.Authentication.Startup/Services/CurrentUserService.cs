namespace AgileTracker.Gateway.Authentication.Startup.Services
{
    using System.Security.Claims;

    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Contracts;

    using Microsoft.AspNetCore.Http;

    public class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            var userId = contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.UserId = userId;
        }

        public string UserId { get; }
    }
}
