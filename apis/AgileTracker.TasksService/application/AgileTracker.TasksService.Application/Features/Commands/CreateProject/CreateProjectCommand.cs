namespace AgileTracker.TasksService.Application.Features.Commands.CreateProject
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class CreateProjectCommand : CreateProjectInputModel, IRequest<Result<CreateProjectOutputModel>>
    {
        public CreateProjectCommand(int projectGroupId, string ownerId, string title)
            : base(projectGroupId, ownerId, title)
        {
        }

        public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<CreateProjectOutputModel>>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public CreateProjectCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result<CreateProjectOutputModel>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
            {
                var projectGroup = await this._projectGroupRepository.GetById(request.ProjectGroupId);

                if (projectGroup == null)
                {
                    throw new NotFoundException("project group", request.ProjectGroupId);
                }

                var owner = projectGroup.Members.FirstOrDefault(g => g.IsOwner);

                if (owner == null || owner.MemberId != request.OwnerId)
                {
                    throw new ModelValidationException();
                }

                var project = projectGroup.AddProject(request.Title);

                await this._projectGroupRepository.Save(projectGroup);

                return Result<CreateProjectOutputModel>.SuccessWith(new CreateProjectOutputModel(project.Id));
            }
        }
    }
}
