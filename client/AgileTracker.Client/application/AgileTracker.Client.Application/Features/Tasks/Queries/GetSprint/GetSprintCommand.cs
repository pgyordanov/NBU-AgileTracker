namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class GetSprintCommand : GetSprintInputModel, IRequest<Result<GetSprintOutputModel>>
    {
        public GetSprintCommand(int projectGroupId, int projectId, int sprintId) : base(projectGroupId, projectId, sprintId)
        {
        }

        public class GetSprintCommandHandler : IRequestHandler<GetSprintCommand, Result<GetSprintOutputModel>>
        {
            private readonly IGatewayService _gatewayService;

            public GetSprintCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<GetSprintOutputModel>> Handle(GetSprintCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.GetSprint(request);
            }
        }
    }
}
