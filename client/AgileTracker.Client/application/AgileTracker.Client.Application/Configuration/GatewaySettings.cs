namespace AgileTracker.Client.Application.Configuration
{
    public class GatewaySettings
    {
        public string BaseAddress { get; set; } = default!;

        public string GetProjectGroupsForMemberEndpoint { get; set; } = default!;

        public string GetUserInfoEndpoint { get; set; } = default!;

        public string IsEmailRegisteredEndpoint { get; set; } = default!;

        public string GetInvitationsForProjectGroupsForMemberEndpoint { get; set; } = default!;

        public string CreateProjectGroupEndpoint { get; set; } = default!;

        public string RemoveProjectGroupEndpoint { get; set; } = default!;

        public string InviteMemberToProjectGroupEndpoint { get; set; } = default!;

        public string AcceptProjectGroupInvitationEndpoint { get; set; } = default!;

        public string CreateProjectEndpoint { get; set; } = default!;

        public string RemoveProjectEndpoint { get; set; } = default!;

        public string GetProjectEndpoint { get; set; } = default!;

        public string AddToBacklogEndpoint { get; set; } = default!;

        public string RemoveFromBacklogEndpoint { get; set; } = default!;

        public string UpdateBacklogTaskEndpoint { get; set; } = default!;

        public string CreateSprintEndpoint { get; set; } = default!;

        public string GetSprintEndpoint { get; set; } = default!;

        public string UpdateSprintTaskStatusEndpoint { get; set; } = default!;

        public string FinishSprintEndpoint { get; set; } = default!;

        public string RemoveSprintEndpoint { get; set; } = default!;

        public string CreateTaskEstimationEndpoint { get; set; } = default!;

        public string GetTaskEstimationsEndpoint { get; set; } = default!;
    }
}
