namespace AgileTracker.StatisticsService.Application.Features.Queries.GetEstimationStatistics
{
    public class GetEstimationStatisticsInputModel
    {
        public GetEstimationStatisticsInputModel(int? projectGroupId, int? projectId, string memberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.MemberId = memberId;
        }

        public int? ProjectGroupId { get; }

        public int? ProjectId { get; }

        public string MemberId { get; }
    }
}
