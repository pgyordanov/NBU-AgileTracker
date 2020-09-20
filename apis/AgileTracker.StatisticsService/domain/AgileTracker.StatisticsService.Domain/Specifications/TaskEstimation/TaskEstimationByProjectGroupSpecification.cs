namespace AgileTracker.StatisticsService.Domain.Specifications.TaskEstimation
{
    using System;
    using System.Linq.Expressions;

    public class TaskEstimationByProjectGroupSpecification : Specification<Models.TaskEstimation.TaskEstimation>
    {
        private readonly int? _projectGroupId;

        public TaskEstimationByProjectGroupSpecification(int? projectGroupId)
        {
            this._projectGroupId = projectGroupId;
        }

        protected override bool Include => this._projectGroupId != null;

        public override Expression<Func<Models.TaskEstimation.TaskEstimation, bool>> ToExpression()
        {
            return taskEstimation => taskEstimation.ProjectGroupId == this._projectGroupId;
        }
    }
}
