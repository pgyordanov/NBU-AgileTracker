namespace AgileTracker.Client.Application.Features.Tasks.Commands.UpdateSprintTaskStatus
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class UpdateSprintTaskStatusCommand : UpdateSprintTaskStatusInputModel, IRequest<Result>
    {
        public UpdateSprintTaskStatusCommand(int projectGroupId, int projectId, int sprintId, int taskId, string taskStatus) 
            : base(projectGroupId, projectId, sprintId, taskId, taskStatus)
        {
        }

        public class UpdateSprintTaskStatusCommandHandler : IRequestHandler<UpdateSprintTaskStatusCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public UpdateSprintTaskStatusCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(UpdateSprintTaskStatusCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.UpdateSprintTaskStatus(request);
            }
        }
    }
}
