namespace AgileTracker.Client.Application.Features.Tasks.Commands.UpdateBacklogTask
{
    using System.Threading;
    using System.Threading.Tasks;
    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class UpdateBacklogTaskCommand : UpdateBacklogTaskInputModel, IRequest<Result>
    {
        public UpdateBacklogTaskCommand(
            int projectGroupId, 
            int projectId,
            int taskId, 
            string title,
            string description, 
            int pointsEstimate, 
            string assignedToMemberId) 
            : base(projectGroupId, projectId, taskId, title, description, pointsEstimate, assignedToMemberId)
        {
        }

        public class UpdateBacklogTaskCommandHandler : IRequestHandler<UpdateBacklogTaskCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public UpdateBacklogTaskCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(UpdateBacklogTaskCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.UpdateBacklogTask(request);
            }
        }
    }
}
