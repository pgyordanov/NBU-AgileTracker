namespace AgileTracker.StatisticsService.Application.Features.Commands.UpdateEstimation
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.StatisticsService.Application.Contracts;

    using MediatR;

    public class UpdateEstimationCommand : UpdateEstimationInputModel, IRequest<Result>
    {
        public UpdateEstimationCommand(int projectGroupId, int projectId, int taskId, string memberId, DateTime estimatedToFinishOn) 
            : base(projectGroupId, projectId, taskId, memberId, estimatedToFinishOn)
        {
        }

        public class UpdateEstimationCommandHandler : IRequestHandler<UpdateEstimationCommand, Result>
        {
            private readonly ITaskEstimationRepository _taskEstimationRepository;

            public UpdateEstimationCommandHandler(ITaskEstimationRepository taskEstimationRepository)
            {
                this._taskEstimationRepository = taskEstimationRepository;
            }

            public async Task<Result> Handle(UpdateEstimationCommand request, CancellationToken cancellationToken)
            {
                bool isOwner = await this._taskEstimationRepository.IsOwner(request.ProjectGroupId, request.MemberId);

                if (!isOwner)
                {
                    throw new ModelValidationException();
                }

                var taskEstimation = 
                    await this._taskEstimationRepository.GetByKeys(request.ProjectGroupId, request.ProjectId, request.TaskId);

                taskEstimation.SetEstimatedToFinishOnTime(request.EstimatedToFinishOn);

                await this._taskEstimationRepository.Save(taskEstimation);

                return true;
            }
        }
    }
}
