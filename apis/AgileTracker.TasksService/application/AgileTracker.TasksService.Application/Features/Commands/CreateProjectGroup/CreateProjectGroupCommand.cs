namespace AgileTracker.TasksService.Application.Features.Commands.CreateProjectGroup
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.TasksService.Application.Contracts;
    using AgileTracker.TasksService.Domain.Factories;

    using MediatR;

    public class CreateProjectGroupCommand : CreateProjectGroupInputModel, IRequest<Result<CreateProjectGroupOutputModel>>
    {
        public CreateProjectGroupCommand(string groupName, string ownerId)
            : base(groupName, ownerId)
        {
        }

        public class CreateProjectGroupCommandHandler : IRequestHandler<CreateProjectGroupCommand, Result<CreateProjectGroupOutputModel>>
        {
            private readonly IProjectGroupFactory _projectGroupFactory;
            private readonly IProjectGroupRepository _projectGroupRepository;

            public CreateProjectGroupCommandHandler(IProjectGroupFactory projectGroupFactory, IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupFactory = projectGroupFactory;
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result<CreateProjectGroupOutputModel>> Handle(CreateProjectGroupCommand request, CancellationToken cancellationToken)
            {
                var projectGroup = this._projectGroupFactory
                                        .WithGroupName(request.GroupName)
                                        .WithOwner(request.OwnerId)
                                        .Build();

                await this._projectGroupRepository.Save(projectGroup, cancellationToken);

                return Result<CreateProjectGroupOutputModel>.SuccessWith(new CreateProjectGroupOutputModel(projectGroup.Id));
            }
        }
    }
}
