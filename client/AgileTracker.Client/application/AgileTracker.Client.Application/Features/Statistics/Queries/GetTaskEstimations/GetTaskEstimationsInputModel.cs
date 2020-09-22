namespace AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstimations
{
    public class GetTaskEstimationsInputModel
    {
        public GetTaskEstimationsInputModel(int? projectGroupId, int? projectId, int? taskId, bool? onlyCompleted)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.OnlyCompleted = onlyCompleted;
        }

        public int? ProjectGroupId { get; }

        public int? ProjectId { get; }

        public int? TaskId { get; }

        public bool? OnlyCompleted { get; }
    }
}
