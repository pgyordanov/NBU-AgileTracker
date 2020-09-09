using AgileTracker.Common.Application.Mapping;

namespace AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.GetUsersInformation
{
    public class UserInformationOutputModel: IMapFrom<IUser>
    {
        public string Id { get; private set; } = default!;

        public string UserName { get; private set; } = default!;

        public string Firstname { get; private set; } = default!;

        public string Lastname { get; private set; } = default!;
    }
}
