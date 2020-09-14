namespace AgileTracker.Client.Application.Features.Tasks.Commands.RemoveProject
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class RemoveProjectCommand : RemoveProjectInputModel, IRequest<Result>
    {
        public RemoveProjectCommand(int projectGroupId, int projectId) : base(projectGroupId, projectId)
        {
        }

        public class RemoveProjectCommandHandler : IRequestHandler<RemoveProjectCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public RemoveProjectCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.RemoveProject(request);
            }
        }
    }
}
