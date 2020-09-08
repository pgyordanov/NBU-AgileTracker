namespace AgileTracker.Client.Startup.Services
{
    using System.Linq;
    using System.Security.Claims;

    using AgileTracker.Client.Application.Contracts;

    using Microsoft.AspNetCore.Http;

    public class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.UserId = userId;
        }

        public string UserId { get; }
    }
}
