namespace AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.IsEmailRegistered
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Contracts;

    using MediatR;

    public class IsEmailRegisteredCommand : IsEmailRegisteredInputModel, IRequest<Result<IsEmailRegisteredOutputModel>>
    {
        public IsEmailRegisteredCommand(string userEmail) : base(userEmail)
        {
        }

        public class IsEmailRegisteredCommandHandler : IRequestHandler<IsEmailRegisteredCommand, Result<IsEmailRegisteredOutputModel>>
        {
            private readonly ICurrentUser currentUserService;
            private readonly IIdentityApi _identityApiService;

            public IsEmailRegisteredCommandHandler(ICurrentUser currentUserService, IIdentityApi identityApiService)
            {
                this.currentUserService = currentUserService;
                this._identityApiService = identityApiService;
            }

            public async Task<Result<IsEmailRegisteredOutputModel>> Handle(IsEmailRegisteredCommand request, CancellationToken cancellationToken)
            {
                return await this._identityApiService.IsEmailRegistered(request);
            }
        }
    }
}
