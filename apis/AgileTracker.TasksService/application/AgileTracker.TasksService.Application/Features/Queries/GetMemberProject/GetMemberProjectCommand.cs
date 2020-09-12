namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProject
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class GetMemberProjectCommand : GetMemberProjectInputModel, IRequest<Result<GetMemberProjectOutputModel>>
    {
        public GetMemberProjectCommand(int projectGroupId, int projectId, string memberId) 
            : base(projectGroupId, projectId, memberId)
        {
        }

        public class GetMemberProjectCommandHandler : IRequestHandler<GetMemberProjectCommand, Result<GetMemberProjectOutputModel>>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public GetMemberProjectCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result<GetMemberProjectOutputModel>> Handle(GetMemberProjectCommand request, CancellationToken cancellationToken)
            {
                bool isMember = await this._projectGroupRepository.IsMember(request.ProjectGroupId, request.MemberId);

                if (!isMember)
                {
                    throw new ModelValidationException();
                }

                var project = await this._projectGroupRepository.GetProject(request.ProjectGroupId, request.ProjectId);

                return Result<GetMemberProjectOutputModel>.SuccessWith(project);
            }
        }
    }
}
