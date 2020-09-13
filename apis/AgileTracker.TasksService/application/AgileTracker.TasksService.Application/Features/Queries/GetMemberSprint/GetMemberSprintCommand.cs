namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberSprint
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class GetMemberSprintCommand : GetMemberSprintInputModel, IRequest<Result<GetMemberSprintOutputModel>>
    {
        public GetMemberSprintCommand(int projectGroupId, int projectId, int sprintId, string memberId) 
            : base(projectGroupId, projectId, sprintId, memberId)
        {
        }

        public class GetMemberSprintCommandHandler : IRequestHandler<GetMemberSprintCommand, Result<GetMemberSprintOutputModel>>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public GetMemberSprintCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result<GetMemberSprintOutputModel>> Handle(GetMemberSprintCommand request, CancellationToken cancellationToken)
            {
                bool isMember = await this._projectGroupRepository.IsMember(request.ProjectGroupId, request.MemberId);

                if (!isMember)
                {
                    throw new ModelValidationException();
                }

                var sprint = await this._projectGroupRepository.GetSprint(request.ProjectGroupId, request.ProjectId, request.SprintId);

                return Result<GetMemberSprintOutputModel>.SuccessWith(sprint);
            }
        }
    }
}
