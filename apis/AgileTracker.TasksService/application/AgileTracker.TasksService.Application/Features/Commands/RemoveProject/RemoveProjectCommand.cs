namespace AgileTracker.TasksService.Application.Features.Commands.RemoveProject
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class RemoveProjectCommand : RemoveProjectInputModel, IRequest<Result>
    {
        public RemoveProjectCommand(int projectGroupId, int projectId, string memberId) 
            : base(projectGroupId, projectId, memberId)
        {
        }

        public class RemoveProjectCommandHandler : IRequestHandler<RemoveProjectCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public RemoveProjectCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
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

                projectGroup.RemoveProject(request.ProjectId);

                await this._projectGroupRepository.Save(projectGroup);

                return true;
            }
        }
    }
}
