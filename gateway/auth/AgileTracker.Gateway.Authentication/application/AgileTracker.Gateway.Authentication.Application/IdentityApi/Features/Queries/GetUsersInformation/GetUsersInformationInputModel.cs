namespace AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.GetUsersInformation
{
    using System.Collections.Generic;

    public class GetUsersInformationInputModel
    {
        public GetUsersInformationInputModel(IEnumerable<string> userIds)
        {
            this.UserIds = userIds;
        }

        public IEnumerable<string> UserIds { get; }
    }
}
