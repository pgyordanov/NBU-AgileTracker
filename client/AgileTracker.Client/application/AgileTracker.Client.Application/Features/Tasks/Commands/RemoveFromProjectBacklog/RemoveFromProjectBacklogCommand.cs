namespace AgileTracker.Client.Application.Features.Tasks.Commands.RemoveFromProjectBacklog
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class RemoveFromProjectBacklogCommand : RemoveFromProjectBacklogInputModel, IRequest<Result>
    {
        public RemoveFromProjectBacklogCommand(int projectGroupId, int projectId, int taskId) 
            : base(projectGroupId, projectId, taskId)
        {
        }

        public class RemoveFromProjectBacklogCommandHandler : IRequestHandler<RemoveFromProjectBacklogCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public RemoveFromProjectBacklogCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(RemoveFromProjectBacklogCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.RemoveFromProjectBacklog(request);
            }
        }
    }
}
