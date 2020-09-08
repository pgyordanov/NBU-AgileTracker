namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity
{
    using System.Linq;
    using System.Security.Claims;

    using AgileTracker.Common.Infrastructure;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    internal class AgileTrackerIdentityDbInitializer : IInitializer
    {
        private readonly AgileTrackerIdentityDbContext _db;
        private readonly UserManager<AgileTrackerUser> _userManager;

        public AgileTrackerIdentityDbInitializer(AgileTrackerIdentityDbContext db, UserManager<AgileTrackerUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        public void Initialize()
        {
            this._db.Database.Migrate();

            var user =
                new AgileTrackerUser
                {
                    UserName = "manager@agiletracker.com",
                    Email = "manager@agiletracker.com",
                    Firstname = "Manager",
                    Lastname = "Manager",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

            if (!this._userManager.Users.Any(s => s.Email == user.Email))
            {
                var identityResult = this._userManager.CreateAsync(user, "123123").GetAwaiter().GetResult();

                this._userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "manager"));
            }
        }
    }
}
