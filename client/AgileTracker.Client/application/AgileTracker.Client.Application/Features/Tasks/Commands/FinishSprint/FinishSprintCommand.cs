namespace AgileTracker.Client.Application.Features.Tasks.Commands.FinishSprint
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class FinishSprintCommand : FinishSprintInputModel, IRequest<Result>
    {
        public FinishSprintCommand(int projectGroupId, int projectId, int sprintId) : base(projectGroupId, projectId, sprintId)
        {
        }

        public class FinishSprintCommandHandler : IRequestHandler<FinishSprintCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public FinishSprintCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(FinishSprintCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.FinishSprint(request);
            }
        }
    }
}
