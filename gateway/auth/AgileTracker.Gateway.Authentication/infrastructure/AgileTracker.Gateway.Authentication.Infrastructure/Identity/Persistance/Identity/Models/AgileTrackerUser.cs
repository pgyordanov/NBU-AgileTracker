namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity.Models
{
    using AgileTracker.Gateway.Authentication.Application;

    using Microsoft.AspNetCore.Identity;

    public class AgileTrackerUser: IdentityUser, IUser
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;
    }
}
