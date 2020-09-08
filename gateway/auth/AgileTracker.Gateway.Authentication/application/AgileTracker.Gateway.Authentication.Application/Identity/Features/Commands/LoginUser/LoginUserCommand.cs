namespace AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LoginUser
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.Identity.Contracts;

    using MediatR;

    public class LoginUserCommand: LoginUserInputModel, IRequest<Result>
    {
        public LoginUserCommand(string emailAddress, string password, bool rememberMe)
            :base(emailAddress, password, rememberMe)
        {
        }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result>
        {
            private readonly IIdentity _identityService;

            public LoginUserCommandHandler(IIdentity identityService)
            {
                this._identityService = identityService;
            }

            public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                return await this._identityService.LoginUser(request);
            }
        }
    }
}
