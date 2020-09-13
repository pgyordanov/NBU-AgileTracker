namespace AgileTracker.TasksService.Application.Features.Commands.UpdateSprintTaskStatus
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class UpdateSprintTaskStatusCommand : UpdateSprintTaskStatusInputModel, IRequest<Result>
    {
        public UpdateSprintTaskStatusCommand(
            int projectGroupId, 
            int projectId, 
            int taskId, 
            int sprintId, 
            string memberId, 
            string taskStatus) 
            : base(projectGroupId, projectId, taskId, sprintId, memberId, taskStatus)
        {
        }

        public class UpdateSprintTaskStatusCommandHandler : IRequestHandler<UpdateSprintTaskStatusCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public UpdateSprintTaskStatusCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result> Handle(UpdateSprintTaskStatusCommand request, CancellationToken cancellationToken)
            {
                bool isMember = await this._projectGroupRepository.IsMember(request.ProjectGroupId, request.MemberId);;

                if (!isMember)
                {
                    throw new ModelValidationException();
                }

                var projectGroup = await this._projectGroupRepository.GetById(request.ProjectGroupId);

                var project = projectGroup.Projects.FirstOrDefault(p => p.Id == request.ProjectId);

                project.ChangeSprintTaskStatus(request.SprintId, request.TaskId, request.TaskStatus);

                await this._projectGroupRepository.Save(projectGroup);

                return true;
            }
        }
    }
}
