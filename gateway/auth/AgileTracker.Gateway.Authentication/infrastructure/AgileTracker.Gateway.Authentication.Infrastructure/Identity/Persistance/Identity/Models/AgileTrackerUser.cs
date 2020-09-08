namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity.Models
{
    using Microsoft.AspNetCore.Identity;

    public class AgileTrackerUser: IdentityUser
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;
    }
}
