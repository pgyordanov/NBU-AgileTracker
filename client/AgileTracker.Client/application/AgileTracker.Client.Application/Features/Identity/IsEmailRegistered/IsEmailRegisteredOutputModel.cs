namespace AgileTracker.Client.Application.Features.Identity.IsEmailRegistered
{
    public class IsEmailRegisteredOutputModel
    {
        public string UserId { get; set; } = default!;

        public bool IsEmailRegistered { get; set; }
    }
}
