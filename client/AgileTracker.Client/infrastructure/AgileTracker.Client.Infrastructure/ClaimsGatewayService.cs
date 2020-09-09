namespace AgileTracker.Client.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Configuration;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Client.Infrastructure.Contracts;
    using AgileTracker.Common.Application;

    using Microsoft.Extensions.Options;

    internal class ClaimsGatewayService : BaseHttpGatewayService, IClaimsGatewayService
    {
        private readonly GatewaySettings _gatewaySettings;

        public ClaimsGatewayService(HttpClient client, IOptions<GatewaySettings> gatewaySettings)
            :base(client)
        {
            this._gatewaySettings = gatewaySettings.Value;
        }

        public async Task<Result<IEnumerable<GetProjectGroupsOutputModel>>> GetProjectGroups(string accessToken)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.GetProjectGroupsForMemberEndpoint),
                Method = HttpMethod.Get
            };

            return await this.MakeAuthenticatedRequest<IEnumerable<GetProjectGroupsOutputModel>>(request, accessToken);
        }
    }
}
