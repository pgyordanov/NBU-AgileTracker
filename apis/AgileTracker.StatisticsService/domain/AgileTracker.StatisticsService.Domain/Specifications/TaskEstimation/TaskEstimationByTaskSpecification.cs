namespace AgileTracker.StatisticsService.Domain.Specifications.TaskEstimation
{
    using System;
    using System.Linq.Expressions;

    public class TaskEstimationByTaskSpecification : Specification<Models.TaskEstimation.TaskEstimation>
    {
        private readonly int? _taskId;

        public TaskEstimationByTaskSpecification(int? taskId)
        {
            this._taskId = taskId;
        }

        protected override bool Include => this._taskId != null;

        public override Expression<Func<Models.TaskEstimation.TaskEstimation, bool>> ToExpression()
        {
            return taskEstimation => taskEstimation.TaskId == this._taskId;
        }
    }
}
