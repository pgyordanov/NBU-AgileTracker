namespace AgileTracker.TasksService.Application.Features.Commands.FinishSprint
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.Common.Events;
    using AgileTracker.Common.Events.Models;
    using AgileTracker.TasksService.Application.Contracts;
    using AgileTracker.TasksService.Domain.Models.Entities;

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
            private readonly IPublishExternalEvent _publishExternalEventService;

            public FinishSprintCommandHandler (IProjectGroupRepository projectGroupRepository, IPublishExternalEvent publishExternalEventService)
            {
                this._projectGroupRepository = projectGroupRepository;
                this._publishExternalEventService = publishExternalEventService;
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

                this.PublishFinishedTaskEvents(request.ProjectGroupId, request.ProjectId, project.Sprints.FirstOrDefault(s => s.Id == request.SprintId));

                return true;
            }

            private void PublishFinishedTaskEvents(int projectGroupId, int projectId, Sprint sprint)
            {
               foreach(var task in sprint.SprintBacklog)
                {
                    if (task.IsFinished)
                    {
                        var eventPayload = new TaskFinishedEventModel
                        {
                            ProjectGroupId = projectGroupId,
                            ProjectId = projectId,
                            TaskId = task.Id,
                            TaskFinishedOn = task.FinishedOn
                        };

                        this._publishExternalEventService.Publish(eventPayload, EventType.TaskFinished);
                    }
                }
            }
        }
    }
}
