namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetUserInfo
{
    public class UserInfoOutputModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool IsOwner { get; set; }
    }
}
