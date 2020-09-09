namespace AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.GetUsersInformation
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Contracts;

    using MediatR;

    public class GetUsersInformationCommand : GetUsersInformationInputModel, IRequest<Result<GetUsersInformationOutputModel>>
    {
        public GetUsersInformationCommand(IEnumerable<string> userIds)
            : base(userIds)
        {
        }

        public class GetUsersInformationCommandHandler 
            : IRequestHandler<GetUsersInformationCommand, Result<GetUsersInformationOutputModel>>
        {
            private readonly ICurrentUser currentUserService;
            private readonly IIdentityApi _identityApiService;

            public GetUsersInformationCommandHandler(ICurrentUser currentUserService, IIdentityApi identityApiService)
            {
                this.currentUserService = currentUserService;
                this._identityApiService = identityApiService;
            }

            public async Task<Result<GetUsersInformationOutputModel>> Handle(GetUsersInformationCommand request, CancellationToken cancellationToken)
            {
                return await this._identityApiService.GetUsersInformation(request);
            }
        }
    }
}
