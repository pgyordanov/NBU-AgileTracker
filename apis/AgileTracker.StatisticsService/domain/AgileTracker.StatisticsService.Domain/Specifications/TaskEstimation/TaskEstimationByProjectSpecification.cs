namespace AgileTracker.StatisticsService.Domain.Specifications.TaskEstimation
{
    using System;
    using System.Linq.Expressions;

    public class TaskEstimationByProjectSpecification : Specification<Models.TaskEstimation.TaskEstimation>
    {
        private readonly int? _projectId;

        public TaskEstimationByProjectSpecification(int? projectId)
        {
            this._projectId = projectId;
        }

        protected override bool Include => this._projectId != null;

        public override Expression<Func<Models.TaskEstimation.TaskEstimation, bool>> ToExpression()
        {
            return taskEstimation => taskEstimation.ProjectId == this._projectId;
        }
    }
}
