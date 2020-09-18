namespace AgileTracker.StatisticsService.Infrastructure.Persistance.Models
{
    public class ProjectGroupOwnership
    {
        internal ProjectGroupOwnership(int projectGroupId, string ownerId)
        {
            this.ExternalProjectGroupId = projectGroupId;
            this.OwnerId = ownerId;
        }

        private ProjectGroupOwnership()
        {
        }

        public int Id { get; private set; }

        public int ExternalProjectGroupId { get; private set; }

        public string OwnerId { get; private set; } = default!;
    }
}
