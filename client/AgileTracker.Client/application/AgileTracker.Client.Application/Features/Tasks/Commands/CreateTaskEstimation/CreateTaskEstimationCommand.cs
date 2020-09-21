namespace AgileTracker.Client.Application.Features.Tasks.Commands.CreateTaskEstimation
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class CreateTaskEstimationCommand : CreateTaskEstimationInputModel, IRequest<Result>
    {
        public CreateTaskEstimationCommand(int projectGroupId, int projectId, int taskId, DateTime startedOn, DateTime estimatedToFinishOn) 
            : base(projectGroupId, projectId, taskId, startedOn, estimatedToFinishOn)
        {
        }

        public class CreateTaskEstimationCommandHandler : IRequestHandler<CreateTaskEstimationCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public CreateTaskEstimationCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(CreateTaskEstimationCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.CreateTaskEstimation(request);
            }
        }
    }
}
