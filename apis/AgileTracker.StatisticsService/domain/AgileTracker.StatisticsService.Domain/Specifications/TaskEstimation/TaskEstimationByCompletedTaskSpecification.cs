using System;
using System.Linq.Expressions;

namespace AgileTracker.StatisticsService.Domain.Specifications.TaskEstimation
{
    public class TaskEstimationByCompletedTaskSpecification : Specification<Models.TaskEstimation.TaskEstimation>
    {
        public override Expression<Func<Models.TaskEstimation.TaskEstimation, bool>> ToExpression()
        {
            return taskEstimation => taskEstimation.ActuallyFinishedOn != default;  
        }
    }
}
