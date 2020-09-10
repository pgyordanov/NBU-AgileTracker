namespace AgileTracker.Client.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using System.Text;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Configuration;
    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetUserInfo;
    using AgileTracker.Client.Infrastructure.Contracts;
    using AgileTracker.Common.Application;

    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;

    public class GatewayService : BaseHttpGatewayService, IGatewayService
    {
        private readonly GatewaySettings _gatewaySettings;
        private readonly ICurrentUser _currentUserService;

        public GatewayService(
            HttpClient httpClient,
            ITokenService tokenService, 
            ICurrentUser currentUserService, 
            IOptions<GatewaySettings> gatewaySettings)
            :base(httpClient, tokenService)
        {
            this._gatewaySettings = gatewaySettings.Value;
            this._currentUserService = currentUserService;
        }

        public async Task<Result<CreateProjectGroupOutputModel>> CreateProjectGroup(CreateProjectGroupInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.CreateProjectGroupEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                    JsonConvert.SerializeObject(new { groupName = input.GroupName, ownerId = this._currentUserService.UserId }),
                    Encoding.UTF8, 
                    "application/json")
            };

            return await this.MakeAuthenticatedRequest<CreateProjectGroupOutputModel>(request);
        }

        public async Task<Result<IEnumerable<GetProjectGroupsOutputModel>>> GetProjectGroups()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.GetProjectGroupsForMemberEndpoint),
                Method = HttpMethod.Get
            };

            return await this.MakeAuthenticatedRequest<IEnumerable<GetProjectGroupsOutputModel>>(request);
        }

        public async Task<Result<GetUserInfoOutputModel>> GetUserInfo(GetUserInfoInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.GetUserInfoEndpoint),
                Method = HttpMethod.Get,
                Content = new StringContent(
                    JsonConvert.SerializeObject(new { userIds = input.UserIds }),
                    Encoding.UTF8,
                    "application/json")
            };

            return await this.MakeAuthenticatedRequest<GetUserInfoOutputModel>(request);
        }
    }
}
