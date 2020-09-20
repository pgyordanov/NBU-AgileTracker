namespace AgileTracker.StatisticsService.Application.Features.Queries.GetTaskEstimations
{
    public class GetTaskEstimationsInputModel
    {
        public GetTaskEstimationsInputModel(int? projectGroupId, int? projectId, int? taskId, string memberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.MemberId = memberId;
        }

        public int? ProjectGroupId { get; }

        public int? ProjectId { get; }

        public int? TaskId { get; }

        public string MemberId { get; }
    }
}
