namespace AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstmationStatistics
{
    public class GetTaskEstimationStatisticsInputModel
    {
        public GetTaskEstimationStatisticsInputModel(int? projectGroupId, int? projectId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
        }

        public int? ProjectGroupId { get; }

        public int? ProjectId { get; }
    }
}
