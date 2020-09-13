namespace AgileTracker.TasksService.Application.Features.Commands.RemoveSprint
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class RemoveSprintCommand : RemoveSprintInputModel, IRequest<Result>
    {
        public RemoveSprintCommand(int projectGroupId, int projectId, int sprintId, string memberId) 
            : base(projectGroupId, projectId, sprintId, memberId)
        {
        }

        public class RemoveSprintCommandHandler : IRequestHandler<RemoveSprintCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public RemoveSprintCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result> Handle(RemoveSprintCommand request, CancellationToken cancellationToken)
            {
                var projectGroup = await this._projectGroupRepository.GetById(request.ProjectGroupId);

                if (projectGroup == null)
                {
                    throw new NotFoundException("project group", request.ProjectGroupId);
                }

                var owner = projectGroup.Members.FirstOrDefault(g => g.IsOwner);

                if (owner == null || owner.MemberId != request.MemberId)
                {
                    throw new ModelValidationException();
                }

                var project = projectGroup.Projects.FirstOrDefault(p => p.Id == request.ProjectId);

                if (project == null)
                {
                    throw new NotFoundException("project", request.ProjectId);
                }

                project.RemoveSprint(request.SprintId);

                await this._projectGroupRepository.Save(projectGroup);

                return true;
            }
        }
    }
}
