namespace AgileTracker.TasksService.Application.Features.Commands.UpdateBacklogTask
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;
    using AgileTracker.TasksService.Domain.Builders;

    using MediatR;

    public class UpdateBacklogTaskCommand : UpdateBacklogTaskInputModel, IRequest<Result>
    {
        public UpdateBacklogTaskCommand(
            int projectGroupId, 
            int projectId,
            int taskId,
            string memberId,
            string title,
            string description,
            int pointsEstimate, 
            string assignedToMemberId) 
            : base(projectGroupId, projectId, taskId, memberId, title, description, pointsEstimate, assignedToMemberId)
        {
        }

        public class UpdateBacklogTaskCommandHandler : IRequestHandler<UpdateBacklogTaskCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;
            private readonly ITaskDescriptionBuilder _taskDescriptionBuilder;

            public UpdateBacklogTaskCommandHandler(
                IProjectGroupRepository projectGroupRepository,
                ITaskDescriptionBuilder taskDescriptionBuilder)
            {
                this._projectGroupRepository = projectGroupRepository;
                this._taskDescriptionBuilder = taskDescriptionBuilder;
            }

            public async Task<Result> Handle(UpdateBacklogTaskCommand request, CancellationToken cancellationToken)
            {
                bool isMember = await this._projectGroupRepository.IsMember(request.ProjectGroupId, request.MemberId);
                bool isAssigneeMember = await this._projectGroupRepository.IsMember(request.ProjectGroupId, request.AssignedToMemberId);

                if (!isMember || !isAssigneeMember)
                {
                    throw new ModelValidationException();
                }

                var projectGroup = await this._projectGroupRepository.GetById(request.ProjectGroupId);

                var project = projectGroup.Projects.FirstOrDefault(p => p.Id == request.ProjectId);

                var taskDescription = this._taskDescriptionBuilder
                                                .WithTitle(request.Title)
                                                .WithDescription(request.Description)
                                                .WithPointsEstimation(request.PointsEstimate)
                                                .WithAssignedToMemberId(request.AssignedToMemberId)
                                                .Build();

                project.UpdateTask(request.TaskId, taskDescription);

                await this._projectGroupRepository.Save(projectGroup);

                return true;
            }
        }
    }
}
