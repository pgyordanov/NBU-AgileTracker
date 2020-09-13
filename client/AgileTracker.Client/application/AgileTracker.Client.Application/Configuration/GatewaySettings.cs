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

        public string AcceptProjectGroupInvitationEndpoint { get; set; }

        public string CreateProjectEndpoint { get; set; }

        public string GetProjectEndpoint { get; set; }

        public string AddToBacklogEndpoint { get; set; }

        public string RemoveFromBacklogEndpoint { get; set; }

        public string CreateSprintEndpoint { get; set; }
    }
}
