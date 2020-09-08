namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroupInvitations
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class GetMemberProjectGroupInvitationsCommand: 
        GetMemberProjectGroupInvitationsInputModel, IRequest<Result<IEnumerable<GetMemberProjectGroupInvitationsOutputModel>>>
    {
        public GetMemberProjectGroupInvitationsCommand(string memberId)
            :base(memberId)
        {
        }

        public class GetMemberProjectGroupInvitationsCommandHandler :
            IRequestHandler<GetMemberProjectGroupInvitationsCommand, Result<IEnumerable<GetMemberProjectGroupInvitationsOutputModel>>>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public GetMemberProjectGroupInvitationsCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }
            public async Task<Result<IEnumerable<GetMemberProjectGroupInvitationsOutputModel>>> Handle(
                GetMemberProjectGroupInvitationsCommand request, 
                CancellationToken cancellationToken)
            {
                var memberProjectGroups = await this._projectGroupRepository.GetMemberInvitedToProjectGroups(request.MemberId);

                return Result<IEnumerable<GetMemberProjectGroupInvitationsOutputModel>>.SuccessWith(memberProjectGroups);
            }
        }
    }
}
