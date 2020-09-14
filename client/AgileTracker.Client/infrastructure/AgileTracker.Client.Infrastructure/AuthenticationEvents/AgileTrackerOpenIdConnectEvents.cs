namespace AgileTracker.Client.Infrastructure.AuthenticationEvents
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using AgileTracker.Client.Infrastructure.Contracts;

    using Microsoft.AspNetCore.Authentication.OpenIdConnect;

    public static class AgileTrackerOpenIdConnectEvents
    {
        public static Func<UserInformationReceivedContext, Task> OnUserInformationReceived
            = (UserInformationReceivedContext context) =>
        {
            var currentUserId = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var accessToken = context.ProtocolMessage.AccessToken;

            var claimsGatewayService = (IClaimsGatewayService)context.HttpContext.RequestServices.GetService(typeof(IClaimsGatewayService));

            try
            {
                var res = claimsGatewayService.GetProjectGroups(accessToken).GetAwaiter().GetResult();

                if (res.Succeeded)
                {
                    var identity = context.Principal.Identity as ClaimsIdentity;

                    foreach (var group in res.Data)
                    {
                        identity!.AddClaim(new Claim($"projectgroup.{group.Id}", "participant"));
                         
                        if (group.Members.Any(m => m.MemberId == currentUserId && m.IsOwner))
                        {
                            identity!.AddClaim(new Claim($"projectgroup.{group.Id}", "owner"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                //err
            }

            return Task.FromResult(0);
        };
    }
}
