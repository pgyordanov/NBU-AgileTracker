namespace AgileTracker.Client.Application.Features.Tasks.Commands.CreateProjectGroup
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class CreateProjectGroupCommand : CreateProjectGroupInputModel, IRequest<Result<CreateProjectGroupOutputModel>>
    {
        public CreateProjectGroupCommand(string groupName) 
            : base(groupName)
        {
        }

        public class CreateProjectGroupCommandHandler : 
            IRequestHandler<CreateProjectGroupCommand, Result<CreateProjectGroupOutputModel>>
        {
            private readonly IGatewayService _gatewayService;

            public CreateProjectGroupCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<CreateProjectGroupOutputModel>> Handle(CreateProjectGroupCommand request, CancellationToken cancellationToken)
            {
                var result = await this._gatewayService.CreateProjectGroup(request);

                return result;
            }
        }
    }
}
