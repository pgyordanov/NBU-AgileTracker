namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class GetProjectGroupsCommand: IRequest<Result<IEnumerable<GetProjectGroupsOutputModel>>>
    {
        public class GetProjectGroupsCommandHandler :
            IRequestHandler<GetProjectGroupsCommand, Result<IEnumerable<GetProjectGroupsOutputModel>>>
        {
            private readonly IGatewayService _gatewayService;
            private readonly ICurrentUser _currentUserService;

            public GetProjectGroupsCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<IEnumerable<GetProjectGroupsOutputModel>>> Handle(GetProjectGroupsCommand request, CancellationToken cancellationToken)
            {
                var result = await this._gatewayService.GetProjectGroups();

                return result;
            }
        }
    }
}
