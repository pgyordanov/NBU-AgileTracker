namespace AgileTracker.StatisticsService.Domain.Specifications.TaskEstimation
{
    using System;
    using System.Linq.Expressions;

    public class TaskEstimationByEstimatorSpecification : Specification<Models.TaskEstimation.TaskEstimation>
    {
        private readonly string _memberId;

        public TaskEstimationByEstimatorSpecification(string memberId)
        {
            this._memberId = memberId;
        }

        public override Expression<Func<Models.TaskEstimation.TaskEstimation, bool>> ToExpression()
        {
            return taskEstimation => taskEstimation.EstimatedByMemberId == this._memberId;  
        }
    }
}
