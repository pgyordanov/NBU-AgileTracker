namespace AgileTracker.TasksService.Application.Features.Commands.InviteMemberToProjectGroup
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    using Newtonsoft.Json;

    public class InviteMemberToProjectGroupCommand : InviteMemberToProjectGroupInputModel, IRequest<Result>
    {
        public InviteMemberToProjectGroupCommand(int projectGroupId, string invitedByMemberId, string memberId)
            : base(projectGroupId, invitedByMemberId, memberId)
        {
        }

        public class InviteMemberToProjectGroupCommandHandler : IRequestHandler<InviteMemberToProjectGroupCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public InviteMemberToProjectGroupCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result> Handle(InviteMemberToProjectGroupCommand request, CancellationToken cancellationToken)
            {
                var projectGroup = await this._projectGroupRepository.GetById(request.ProjectGroupId);

                if (projectGroup == null)
                {
                    throw new NotFoundException("project group", request.ProjectGroupId);
                }

                var owner = projectGroup.Members.FirstOrDefault(g => g.IsOwner);

                if (owner == null || owner.MemberId != request.InvitedByMemberId)
                {
                    throw new ModelValidationException();
                }

                projectGroup.AddInvitation(request.MemberId);

                await this._projectGroupRepository.Save(projectGroup);

                return true;
            }
        }
    }
}
