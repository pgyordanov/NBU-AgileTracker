namespace AgileTracker.Client.Application.Features.Statistics.Commands.UpdateTaskEstimation
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class UpdateTaskEstimationCommand : UpdateTaskEstimationInputModel, IRequest<Result>
    {
        public UpdateTaskEstimationCommand(int projectGroupId, int projectId, int taskId, DateTime estimatedToFinishOn) : base(projectGroupId, projectId, taskId, estimatedToFinishOn)
        {
        }

        public class UpdateTaskEstimationCommandHandler : IRequestHandler<UpdateTaskEstimationCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public UpdateTaskEstimationCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(UpdateTaskEstimationCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.UpdateTaskEstimation(request);
            }
        }
    }
}
