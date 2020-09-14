namespace AgileTracker.Client.Application.Features.Tasks.Commands.RemoveSprint
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class RemoveSprintCommand : RemoveSprintInputModel, IRequest<Result>
    {
        public RemoveSprintCommand(int projectGroupId, int projectId, int sprintId) 
            : base(projectGroupId, projectId, sprintId)
        {
        }

        public class RemoveSprintCommandHandler : IRequestHandler<RemoveSprintCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public RemoveSprintCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(RemoveSprintCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.RemoveSprint(request);
            }
        }
    }
}
