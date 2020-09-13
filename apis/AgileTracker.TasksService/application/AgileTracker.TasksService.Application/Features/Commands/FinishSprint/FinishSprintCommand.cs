namespace AgileTracker.TasksService.Application.Features.Commands.FinishSprint
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class FinishSprintCommand : FinishSprintInputModel, IRequest<Result>
    {
        public FinishSprintCommand(int projectGroupId, int projectId, int sprintId, string memberId) 
            : base(projectGroupId, projectId, sprintId, memberId)
        {
        }

        public class FinishSprintCommandHandler : IRequestHandler<FinishSprintCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public FinishSprintCommandHandler (IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result> Handle(FinishSprintCommand request, CancellationToken cancellationToken)
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

                project.FinishSprint(request.SprintId);

                await this._projectGroupRepository.Save(projectGroup);

                return true;
            }
        }
    }
}
