namespace AgileTracker.Client.Application.Features.Tasks.Commands.RemoveProjectGroup
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class RemoveProjectGroupCommand : RemoveProjectGroupInputModel, IRequest<Result>
    {
        public RemoveProjectGroupCommand(int projectGroupId) : base(projectGroupId)
        {
        }

        public class RemoveProjectGroupCommandHandler : IRequestHandler<RemoveProjectGroupCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public RemoveProjectGroupCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(RemoveProjectGroupCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.RemoveProjectGroup(request);
            }
        }
    }
}
