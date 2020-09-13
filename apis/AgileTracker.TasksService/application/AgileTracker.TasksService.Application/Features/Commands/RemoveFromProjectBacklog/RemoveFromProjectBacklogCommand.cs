namespace AgileTracker.TasksService.Application.Features.Commands.RemoveFromProjectBacklog
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class RemoveFromProjectBacklogCommand : RemoveFromProjectBacklogInputModel, IRequest<Result>
    {
        public RemoveFromProjectBacklogCommand(int projectGroupId, int projectId, string memberId, int taskId) 
            : base(projectGroupId, projectId, memberId, taskId)
        {
        }

        public class RemoveFromProjectBacklogCommandHandler : IRequestHandler<RemoveFromProjectBacklogCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public RemoveFromProjectBacklogCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result> Handle(RemoveFromProjectBacklogCommand request, CancellationToken cancellationToken)
            {
                bool isMember = await this._projectGroupRepository.IsMember(request.ProjectGroupId, request.MemberId);

                if (!isMember)
                {
                    throw new ModelValidationException();
                }

                var projectGroup = await this._projectGroupRepository.GetById(request.ProjectGroupId);

                var project = projectGroup.Projects.FirstOrDefault(p => p.Id == request.ProjectId);

                project.RemoveFromBacklog(request.TaskId);

                await this._projectGroupRepository.Save(projectGroup);

                return true;
            }
        }
    }
}
