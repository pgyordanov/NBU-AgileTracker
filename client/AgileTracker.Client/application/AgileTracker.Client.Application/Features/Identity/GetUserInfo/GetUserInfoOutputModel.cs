namespace AgileTracker.Client.Application.Features.Identity.GetUserInfo
{
    using System.Collections.Generic;

    public class GetUserInfoOutputModel
    {
        public IEnumerable<UserInfoOutputModel> UserInfo { get; set; }
    }
}
