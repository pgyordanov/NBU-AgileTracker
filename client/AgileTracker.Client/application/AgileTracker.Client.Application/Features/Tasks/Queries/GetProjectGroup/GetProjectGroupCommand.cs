namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Client.Application.Features.Identity.GetUserInfo;
    using AgileTracker.Common.Application;

    using MediatR;

    public class GetProjectGroupCommand : GetProjectGroupInputModel, IRequest<Result<GetProjectGroupOutputModel>>
    {
        public GetProjectGroupCommand(int groupId)
            : base(groupId)
        {
        }

        public class GetProjectGroupCommandHandler :
            IRequestHandler<GetProjectGroupCommand, Result<GetProjectGroupOutputModel>>
        {
            private readonly IGatewayService _gatewayService;

            public GetProjectGroupCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<GetProjectGroupOutputModel>> Handle(GetProjectGroupCommand request, CancellationToken cancellationToken)
            {
                var result = await this._gatewayService.GetProjectGroups();

                if (!result.Succeeded)
                {
                    return Result<GetProjectGroupOutputModel>.Failure(result.Errors);
                }

                var group = result.Data.FirstOrDefault(g => g.Id == request.GroupId);

                var userInfoResult = await this._gatewayService
                    .GetUserInfo(new GetUserInfoInputModel(group.Members.Select(m => m.MemberId).ToList()));

                if (!userInfoResult.Succeeded)
                {
                    return Result<GetProjectGroupOutputModel>.Failure(userInfoResult.Errors);
                }

                var model = new GetProjectGroupOutputModel
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    Members = userInfoResult.Data.UserInfo.Select(u=> new ProjectGroupMemberOutputModel
                    {
                        Id = group.Members.First(gm => gm.MemberId == u.Id).Id,
                        MemberId = u.Id,
                        UserName = u.UserName,
                        Firstname = u.Firstname,
                        Lastname = u.Lastname,
                        IsOwner = group.Members.First(gm=> gm.MemberId == u.Id).IsOwner
                    })
                };

                var groupResult = Result<GetProjectGroupOutputModel>.SuccessWith(model);

                return await Task.FromResult(groupResult);
            }
        }
    }
}
