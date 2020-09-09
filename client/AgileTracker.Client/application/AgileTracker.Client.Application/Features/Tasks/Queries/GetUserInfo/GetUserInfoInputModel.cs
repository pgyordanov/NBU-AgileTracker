namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetUserInfo
{
    using System.Collections.Generic;

    public class GetUserInfoInputModel
    {
        public GetUserInfoInputModel(IEnumerable<string> userIds)
        {
            this.UserIds = userIds;
        }

        public IEnumerable<string> UserIds { get; }
    }
}
