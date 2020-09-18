namespace AgileTracker.StatisticsService.Application.Features.Commands.CreateEstimation
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.StatisticsService.Application.Contracts;
    using AgileTracker.StatisticsService.Domain.Repositories;

    using MediatR;

    public class CreateEstimationCommand : CreateEstimationInputModel, IRequest<Result>
    {
        public CreateEstimationCommand(int projectGroupId, int projectId, int taskId, string memberId, DateTime startedOn, DateTime estimatedToFinishOn)
            : base(projectGroupId, projectId, taskId, memberId, startedOn, estimatedToFinishOn)
        {
        }

        public class CreateEstimationCommandHandler : IRequestHandler<CreateEstimationCommand, Result>
        {
            private readonly ITaskEstimationFactory _taskEstimationFactory;
            private readonly ITaskEstimationRepository _taskEstimationRepository;

            public CreateEstimationCommandHandler(ITaskEstimationFactory taskEstimationFactory, ITaskEstimationRepository taskEstimationRepository)
            {
                this._taskEstimationFactory = taskEstimationFactory;
                this._taskEstimationRepository = taskEstimationRepository;
            }

            public async Task<Result> Handle(CreateEstimationCommand request, CancellationToken cancellationToken)
            {
                bool isOwner = await this._taskEstimationRepository.IsOwner(request.ProjectGroupId, request.EstimatedByMemberId);

                if (!isOwner)
                {
                    throw new ModelValidationException();
                }

                var taskEstimation = this._taskEstimationFactory
                                            .WithKeys(request.ProjectGroupId, request.ProjectId, request.TaskId)
                                            .WithEstimatorId(request.EstimatedByMemberId)
                                            .WithStartTime(request.StartedOn)
                                            .WithEstimatedFinishTime(request.EstimatedToFinishOn)
                                            .Build();

                await this._taskEstimationRepository.Save(taskEstimation);

                return true;
            }
        }
    }
}
