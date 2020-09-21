namespace AgileTracker.StatisticsService.Domain.Specifications.TaskEstimation
{
    using System;
    using System.Linq.Expressions;

    public class TaskEstimationByCompletedTaskSpecification : Specification<Models.TaskEstimation.TaskEstimation>
    {
        private readonly bool? _onlyCompleted;

        public TaskEstimationByCompletedTaskSpecification(bool? onlyCompleted)
        {
            this._onlyCompleted = onlyCompleted;
        }

        protected override bool Include => this._onlyCompleted != null && this._onlyCompleted == true;

        public override Expression<Func<Models.TaskEstimation.TaskEstimation, bool>> ToExpression()
        {
            return taskEstimation => taskEstimation.ActuallyFinishedOn != default;  
        }
    }
}
