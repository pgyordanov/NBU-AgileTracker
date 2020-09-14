namespace AgileTracker.Client.Application.Features.Identity.GetUserInfo
{
    public class UserInfoOutputModel
    {
        public string Id { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public bool IsOwner { get; set; }
    }
}
