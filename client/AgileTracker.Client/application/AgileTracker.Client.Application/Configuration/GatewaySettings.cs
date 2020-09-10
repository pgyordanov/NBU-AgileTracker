namespace AgileTracker.Client.Application.Configuration
{
    public class GatewaySettings
    {
        public string BaseAddress { get; set; }

        public string GetProjectGroupsForMemberEndpoint { get; set; }

        public string GetUserInfoEndpoint { get; set; }

        public string IsEmailRegisteredEndpoint { get; set; }

        public string GetInvitationsForProjectGroupsForMemberEndpoint { get; set; }

        public string CreateProjectGroupEndpoint { get; set; }

        public string InviteMemberToProjectGroupEndpoint { get; set; }

        public string AddMemberToProjectGroupEndpoint { get; set; }
    }
}
