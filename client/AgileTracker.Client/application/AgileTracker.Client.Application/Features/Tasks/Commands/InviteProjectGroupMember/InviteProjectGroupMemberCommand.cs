namespace AgileTracker.Client.Application.Features.Tasks.Commands.InviteProjectGroupMember
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Client.Application.Features.Identity.IsEmailRegistered;
    using AgileTracker.Common.Application;

    using MediatR;

    public class InviteProjectGroupMemberCommand : InviteProjectGroupMemberInputModel, IRequest<Result>
    {
        public InviteProjectGroupMemberCommand(int projectGroupId, string memberEmail) 
            : base(projectGroupId, memberEmail)
        {
        }

        public class InviteProjectGroupMemberCommandHandler : IRequestHandler<InviteProjectGroupMemberCommand, Result>
        {
            private readonly IGatewayService _gatewayService;

            public InviteProjectGroupMemberCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result> Handle(InviteProjectGroupMemberCommand request, CancellationToken cancellationToken)
            {
                var isEmailRegisteredRes = await this._gatewayService
                    .IsEmailRegistered(new IsEmailRegisteredInputModel(request.MemberEmail));

                if (!isEmailRegisteredRes.Succeeded)
                {
                    return Result.Failure(isEmailRegisteredRes.Errors);
                }

                if (!isEmailRegisteredRes.Data.IsEmailRegistered)
                {
                    return Result.Failure(new List<string> { "Email is not registered"});
                }

                var userId = isEmailRegisteredRes.Data.UserId;

                return await this._gatewayService.InviteMemberToProjectGroup(request.ProjectGroupId, userId);
            }
        }
    }
}
