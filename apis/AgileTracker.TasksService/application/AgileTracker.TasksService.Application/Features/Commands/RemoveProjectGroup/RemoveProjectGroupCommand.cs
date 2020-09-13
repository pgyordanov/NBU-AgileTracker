namespace AgileTracker.TasksService.Application.Features.Commands.RemoveProjectGroup
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class RemoveProjectGroupCommand : RemoveProjectGroupInputModel, IRequest<Result>
    {
        public RemoveProjectGroupCommand(int projectGroupId, string memberId) 
            : base(projectGroupId, memberId)
        {
        }

        public class RemoveProjectGroupCommandHandler : IRequestHandler<RemoveProjectGroupCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public RemoveProjectGroupCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result> Handle(RemoveProjectGroupCommand request, CancellationToken cancellationToken)
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

                await this._projectGroupRepository.Remove(projectGroup);

                return true;
            }
        }
    }
}
