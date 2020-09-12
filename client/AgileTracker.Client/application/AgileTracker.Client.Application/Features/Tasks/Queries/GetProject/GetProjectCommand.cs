namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProject
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Client.Application.Features.Identity.GetUserInfo;
    using AgileTracker.Common.Application;

    using MediatR;

    public class GetProjectCommand : GetProjectInputModel, IRequest<Result<GetProjectOutputModel>>
    {
        public GetProjectCommand(int projectGroupId, int projectId) : base(projectGroupId, projectId)
        {
        }

        public class GetProjectCommandHandler : IRequestHandler<GetProjectCommand, Result<GetProjectOutputModel>>
        {
            private readonly IGatewayService _gatewayService;

            public GetProjectCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<GetProjectOutputModel>> Handle(GetProjectCommand request, CancellationToken cancellationToken)
            {
                var projectResult = await this._gatewayService.GetProject(request);

                if(!projectResult.Succeeded)
                {
                    return Result<GetProjectOutputModel>.Failure(projectResult.Errors);
                }

                var userInfoResult = await this._gatewayService
                    .GetUserInfo(new GetUserInfoInputModel(projectResult.Data.Members.Select(m => m.MemberId).ToList()));

                if (!userInfoResult.Succeeded)
                {
                    return Result<GetProjectOutputModel>.Failure(userInfoResult.Errors);
                }

                projectResult.Data.Members = userInfoResult.Data.UserInfo.Select(u => new ProjectMemberOutputModel
                {
                    Id = projectResult.Data.Members.First(gm => gm.MemberId == u.Id).Id,
                    MemberId = u.Id,
                    UserName = u.UserName,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    IsOwner = projectResult.Data.Members.First(gm => gm.MemberId == u.Id).IsOwner
                }).ToList();

                return projectResult;
            }
        }
    }
}
