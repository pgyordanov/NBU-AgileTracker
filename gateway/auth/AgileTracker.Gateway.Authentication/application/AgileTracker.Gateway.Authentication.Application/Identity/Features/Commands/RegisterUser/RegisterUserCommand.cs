namespace AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.RegisterUser
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.Identity.Contracts;
    using MediatR;

    public class RegisterUserCommand: RegisterUserInputModel, IRequest<Result>
    {
        public RegisterUserCommand(string firstname, string lastname, string emailAddress, string password)
            :base(firstname, lastname, emailAddress, password)
        {
        }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
        {
            private readonly IIdentity _identityService;

            public RegisterUserCommandHandler(IIdentity identityService)
            {
                this._identityService = identityService;
            }

            public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                return await this._identityService.RegisterUser(request);
            }
        }
    }
}
