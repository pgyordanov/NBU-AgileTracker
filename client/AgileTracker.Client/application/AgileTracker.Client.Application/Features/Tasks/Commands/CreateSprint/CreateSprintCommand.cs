namespace AgileTracker.Client.Application.Features.Tasks.Commands.CreateSprint
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class CreateSprintCommand : CreateSprintInputModel, IRequest<Result<CreateSprintOutputModel>>
    {
        public CreateSprintCommand(int projectGroupId, int projectId, int durationWeeks, IEnumerable<int> taskIds, DateTime startsOn) 
            : base(projectGroupId, projectId, durationWeeks, taskIds, startsOn)
        {
        }

        public class CreateSprintCommandHandler : IRequestHandler<CreateSprintCommand, Result<CreateSprintOutputModel>>
        {
            private readonly IGatewayService _gatewayService;

            public CreateSprintCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<CreateSprintOutputModel>> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.CreateSprint(request);
            }
        }
    }
}
