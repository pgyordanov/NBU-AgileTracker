namespace AgileTracker.Client.Application.Features.Tasks.Commands.CreateProject
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class CreateProjectCommand : CreateProjectInputModel, IRequest<Result<CreateProjectOutputModel>>
    {
        public CreateProjectCommand(int projectGroupId, string projectTitle) : base(projectGroupId, projectTitle)
        {
        }

        public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<CreateProjectOutputModel>>
        {
            private readonly IGatewayService _gatewayService;

            public CreateProjectCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<CreateProjectOutputModel>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.CreateProject(request);
            }
        }
    }
}
