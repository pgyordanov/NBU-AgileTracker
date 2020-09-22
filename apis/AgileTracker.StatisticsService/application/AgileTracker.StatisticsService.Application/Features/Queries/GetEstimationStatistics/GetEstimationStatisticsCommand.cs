namespace AgileTracker.StatisticsService.Application.Features.Queries.GetEstimationStatistics
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.StatisticsService.Application.Contracts;
    using AgileTracker.StatisticsService.Domain.Specifications.TaskEstimation;

    using MediatR;

    public class GetEstimationStatisticsCommand : GetEstimationStatisticsInputModel, IRequest<Result<GetEstimationStatisticsOutputModel>>
    {
        public GetEstimationStatisticsCommand(int? projectGroupId, int? projectId, string memberId)
            : base(projectGroupId, projectId, memberId)
        {
        }

        public class GetEstimationStatisticsCommandHandler : IRequestHandler<GetEstimationStatisticsCommand, Result<GetEstimationStatisticsOutputModel>>
        {
            private readonly ITaskEstimationRepository _taskEstimationRepository;

            public GetEstimationStatisticsCommandHandler(ITaskEstimationRepository taskEstimationRepository)
            {
                this._taskEstimationRepository = taskEstimationRepository;
            }

            public async Task<Result<GetEstimationStatisticsOutputModel>> Handle(GetEstimationStatisticsCommand request, CancellationToken cancellationToken)
            {
                var specification = new TaskEstimationByEstimatorSpecification(request.MemberId)
                                          .And(new TaskEstimationByProjectGroupSpecification(request.ProjectGroupId))
                                          .And(new TaskEstimationByProjectSpecification(request.ProjectId))
                                          .And(new TaskEstimationByCompletedTaskSpecification(true));

                var estimations = await this._taskEstimationRepository.GetTaskEstimations(specification);

                var daysEstimatedVsActualDataSet = estimations.Select(e => new EstimationDataSetEntryOutputModel
                                                        (
                                                          (e.EstimatedToFinishOn.Date - e.StartedOn.Date).Days,
                                                          "Estimation (days)",
                                                          (e.EstimatedToFinishOn.Date - e.StartedOn.Date).Days 
                                                                    - (e.ActuallyFinishedOn.Date - e.StartedOn.Date).Days,
                                                          "Difference estimation vs actual (days)"
                                                        ))      
                                          .ToList();

                var result = new GetEstimationStatisticsOutputModel
                    (
                        estimations.Count(),
                        estimations.Count(e => (e.ActuallyFinishedOn.Date - e.EstimatedToFinishOn.Date).Days == 0),
                        estimations.Count(e=>(e.ActuallyFinishedOn.Date - e.EstimatedToFinishOn.Date).Days >= 1 && (e.ActuallyFinishedOn.Date - e.EstimatedToFinishOn.Date).Days <= 3),
                        estimations.Count(e => (e.ActuallyFinishedOn.Date - e.EstimatedToFinishOn.Date).Days <= -1 && (e.ActuallyFinishedOn.Date - e.EstimatedToFinishOn.Date).Days >= -3),
                        estimations.Count(e => (e.ActuallyFinishedOn.Date - e.EstimatedToFinishOn.Date).Days >= 3),
                        estimations.Count(e => (e.ActuallyFinishedOn.Date - e.EstimatedToFinishOn.Date).Days <= -3),
                        daysEstimatedVsActualDataSet
                    );

                return Result<GetEstimationStatisticsOutputModel>.SuccessWith(result);
            }
        }
    }
}
