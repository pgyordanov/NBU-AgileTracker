namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroupInvitations
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class GetProjectGroupInvitationsCommand: IRequest<Result<IEnumerable<GetProjectGroupInvitationsOutputModel>>>
    {
        public class GetProjectGroupInvitationsCommandHandler :
            IRequestHandler<GetProjectGroupInvitationsCommand, Result<IEnumerable<GetProjectGroupInvitationsOutputModel>>>
        {
            private readonly IGatewayService _gatewayService;

            public GetProjectGroupInvitationsCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<IEnumerable<GetProjectGroupInvitationsOutputModel>>> Handle(GetProjectGroupInvitationsCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.GetProjectGroupInvitations();
            }
        }
    }
}
