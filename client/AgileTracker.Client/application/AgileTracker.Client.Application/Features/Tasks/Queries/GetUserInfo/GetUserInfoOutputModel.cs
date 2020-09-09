namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetUserInfo
{
    using System.Collections.Generic;

    public class GetUserInfoOutputModel
    {
        public IEnumerable<UserInfoOutputModel> UserInfo { get; set; }
    }
}
