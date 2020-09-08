namespace AgileTracker.TasksService.Application.Features.Commands.AddMemberToProjectGroup
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class AddMemberToProjectGroupCommand: AddMemberToProjectGroupInputModel, IRequest<Result>
    {
        public AddMemberToProjectGroupCommand(int projectGroupId, string memberId)
            :base(projectGroupId, memberId)
        {
        }

        public class AddMemberToProjectGroupCommandHandler : IRequestHandler<AddMemberToProjectGroupCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public AddMemberToProjectGroupCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result> Handle(AddMemberToProjectGroupCommand request, CancellationToken cancellationToken)
            {
                var projectGroup = await this._projectGroupRepository.GetById(request.ProjectGroupId);

                if(projectGroup == null)
                {
                    throw new NotFoundException("project group", request.ProjectGroupId);
                }

                projectGroup.AddGroupMember(request.MemberId);

                await this._projectGroupRepository.Save(projectGroup);

                return true;
            }
        }
    }
}
