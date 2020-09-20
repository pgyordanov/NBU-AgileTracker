namespace AgileTracker.StatisticsService.Application.Features.Queries.GetTaskEstimations
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.StatisticsService.Application.Contracts;
    using AgileTracker.StatisticsService.Domain.Specifications.TaskEstimation;

    using MediatR;

    public class GetTaskEstimationsCommand : GetTaskEstimationsInputModel, IRequest<Result<IEnumerable<GetTaskEstimationsOutputModel>>>
    {
        public GetTaskEstimationsCommand(int? projectGroupId, int? projectId, int? taskId, string memberId) 
            : base(projectGroupId, projectId, taskId, memberId)
        {
        }

        public class GetTaskEstimationsCommandHandler : IRequestHandler<GetTaskEstimationsCommand, Result<IEnumerable<GetTaskEstimationsOutputModel>>>
        {
            private readonly ITaskEstimationRepository _taskEstimationRepository;

            public GetTaskEstimationsCommandHandler(ITaskEstimationRepository taskEstimationRepository)
            {
                this._taskEstimationRepository = taskEstimationRepository;
            }

            public async Task<Result<IEnumerable<GetTaskEstimationsOutputModel>>> Handle(GetTaskEstimationsCommand request, CancellationToken cancellationToken)
            {
                var specification = new TaskEstimationByCompletedTaskSpecification()
                                        .And(new TaskEstimationByEstimatorSpecification(request.MemberId))
                                        .And(new TaskEstimationByProjectGroupSpecification(request.ProjectGroupId))
                                        .And(new TaskEstimationByProjectSpecification(request.ProjectId))
                                        .And(new TaskEstimationByTaskSpecification(request.TaskId));

                var estimations = await this._taskEstimationRepository.GetTaskEstimations(specification);

                return Result<IEnumerable<GetTaskEstimationsOutputModel>>.SuccessWith(estimations);
            }
        }
    }
}
