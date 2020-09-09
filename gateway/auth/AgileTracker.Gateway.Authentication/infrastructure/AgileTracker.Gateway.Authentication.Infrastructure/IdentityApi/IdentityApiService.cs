namespace AgileTracker.Gateway.Authentication.Infrastructure.IdentityApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Contracts;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.GetUsersInformation;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity.Models;

    using AutoMapper;

    using Microsoft.AspNetCore.Identity;

    public class IdentityApiService : IIdentityApi
    {
        private readonly UserManager<AgileTrackerUser> _userManager;
        private readonly IMapper _mapper;

        public IdentityApiService(UserManager<AgileTrackerUser> userManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._mapper = mapper;
        }

        public Task<Result<GetUsersInformationOutputModel>> GetUsersInformation(GetUsersInformationInputModel input)
        {
            var users = this._userManager.Users.Where(u => input.UserIds.Contains(u.Id));

            var userInfo = this._mapper.ProjectTo<UserInformationOutputModel>(users);

            var result = Result<GetUsersInformationOutputModel>.SuccessWith(new GetUsersInformationOutputModel(userInfo));

            return Task.FromResult(result);
        }
    }
}
