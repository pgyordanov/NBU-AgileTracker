namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProject
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class GetProjectCommand : GetProjectInputModel, IRequest<Result<GetProjectOutputModel>>
    {
        public GetProjectCommand(int projectGroupId, int projectId) : base(projectGroupId, projectId)
        {
        }

        public class GetProjectCommandHandler : IRequestHandler<GetProjectCommand, Result<GetProjectOutputModel>>
        {
            private readonly IGatewayService _gatewayService;

            public GetProjectCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<GetProjectOutputModel>> Handle(GetProjectCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.GetProject(request);
            }
        }
    }
}
