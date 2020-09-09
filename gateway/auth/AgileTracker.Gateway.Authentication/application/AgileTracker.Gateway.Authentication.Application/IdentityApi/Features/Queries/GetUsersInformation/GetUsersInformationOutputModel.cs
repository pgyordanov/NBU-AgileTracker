namespace AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.GetUsersInformation
{
    using System.Collections.Generic;

    public class GetUsersInformationOutputModel
    {
        public GetUsersInformationOutputModel(IEnumerable<UserInformationOutputModel> userInfo)
        {
            this.UserInfo = userInfo;
        }

        public IEnumerable<UserInformationOutputModel> UserInfo { get; private set; } = default!;
    }
}
