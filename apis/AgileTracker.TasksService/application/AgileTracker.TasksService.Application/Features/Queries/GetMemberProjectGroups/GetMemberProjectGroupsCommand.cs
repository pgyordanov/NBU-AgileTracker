namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.TasksService.Application.Contracts;

    using MediatR;

    public class GetMemberProjectGroupsCommand: GetMemberProjectGroupsInputModel, IRequest<Result<IEnumerable<GetMemberProjectGroupsOutputModel>>>
    {
        public GetMemberProjectGroupsCommand(string memberId)
            :base(memberId)
        {
        }

        public class GetMemberProjectGroupsCommandHandler : IRequestHandler<GetMemberProjectGroupsCommand, Result<IEnumerable<GetMemberProjectGroupsOutputModel>>>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;

            public GetMemberProjectGroupsCommandHandler(IProjectGroupRepository projectGroupRepository)
            {
                this._projectGroupRepository = projectGroupRepository;
            }

            public async Task<Result<IEnumerable<GetMemberProjectGroupsOutputModel>>> Handle(GetMemberProjectGroupsCommand request, CancellationToken cancellationToken)
            {
                var memberProjectGroups = await this._projectGroupRepository.GetMemberProjectGroups(request.MemberId);

                return Result<IEnumerable<GetMemberProjectGroupsOutputModel>>.SuccessWith(memberProjectGroups);
            }
        }
    }
}
