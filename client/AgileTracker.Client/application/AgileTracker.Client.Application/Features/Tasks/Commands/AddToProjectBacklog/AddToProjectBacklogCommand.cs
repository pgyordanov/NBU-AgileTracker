namespace AgileTracker.Client.Application.Features.Tasks.Commands.AddToProjectBacklog
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class AddToProjectBacklogCommand : AddToProjectBacklogInputModel, IRequest<Result>
    {
        public AddToProjectBacklogCommand(
            int projectGroupId, 
            int projectId,
            string title, 
            string description, 
            int pointsEstimate,
            string assignedToMemberId, 
            DateTime startsOn) 
            : base(projectGroupId, projectId, title, description, pointsEstimate, assignedToMemberId, startsOn)
        {
        }

        public class AddToProjectBacklogCommandHandler : IRequestHandler<AddToProjectBacklogCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public AddToProjectBacklogCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(AddToProjectBacklogCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.AddToProjectBacklog(request);
            }
        }
    }
}
