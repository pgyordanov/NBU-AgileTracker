namespace AgileTracker.Client.Application.Features.Tasks.Commands.AcceptProjectGroupInvitation
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;
    using MediatR;

    public class AcceptProjectGroupInvitationCommand : AcceptProjectGroupInvitationInputModel, IRequest<Result>
    {
        public AcceptProjectGroupInvitationCommand(int groupId) : base(groupId)
        {
        }

        public class AcceptProjectGroupInvitationCommandHandler : IRequestHandler<AcceptProjectGroupInvitationCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public AcceptProjectGroupInvitationCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(AcceptProjectGroupInvitationCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.AcceptProjectGroupInvitation(request);
            }
        }
    }
}
