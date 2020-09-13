namespace AgileTracker.TasksService.Application.Features.Commands.CreateSprint
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class CreateSprintCommand : CreateSprintInputModel, IRequest<Result<CreateSprintOutputModel>>
    {
        public CreateSprintCommand(int projectGroupId, int projectId, string memberId, IEnumerable<int> taskIds, DateTime startsOn, int durationWeeks)
            : base(projectGroupId, projectId, memberId, taskIds, startsOn, durationWeeks)
        {
        }

        public class CreateSprintCommandHandler : IRequestHandler<CreateSprintCommand, Result<CreateSprintOutputModel>>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public CreateSprintCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result<CreateSprintOutputModel>> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
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

                var sprint = project.CreateSprint(request.TaskIds, request.StartsOn, request.DurationWeeks);

                await this._projectGroupRepository.Save(projectGroup);

                return Result<CreateSprintOutputModel>.SuccessWith(new CreateSprintOutputModel(sprint.Id));
            }
        }
    }
}
