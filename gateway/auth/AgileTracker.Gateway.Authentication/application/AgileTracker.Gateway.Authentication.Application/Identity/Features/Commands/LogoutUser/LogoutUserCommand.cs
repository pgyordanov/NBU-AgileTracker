namespace AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LogoutUser
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.Identity.Contracts;

    using MediatR;

    public class LogoutUserCommand: LogoutUserInputModel, IRequest<Result<LogoutUserOutputModel>>
    {
        public LogoutUserCommand(string logoutId)
            :base(logoutId)
        {
        }

        public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Result<LogoutUserOutputModel>>
        {
            private readonly IIdentity _identityService;

            public LogoutUserCommandHandler(IIdentity identityService)
            {
                this._identityService = identityService;
            }

            public async Task<Result<LogoutUserOutputModel>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
            {
                return await this._identityService.LogoutUser(request);
            }
        }
    }
}
