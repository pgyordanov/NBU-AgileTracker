namespace AgileTracker.Client.Infrastructure
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AgileTracker.Client.Infrastructure.Contracts;
    using AgileTracker.Common.Application;
    using IdentityModel.Client;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public abstract class BaseHttpGatewayService
    {
        private readonly HttpClient _httpClient;
        private string _accessToken;

        public BaseHttpGatewayService(HttpClient httpClient, ITokenService tokenService)
        {
            this._httpClient = httpClient;
            this._accessToken = tokenService.AccessToken;
        }

        public BaseHttpGatewayService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        protected async Task<Result> MakeAuthenticatedRequest(HttpRequestMessage request)
        {
            this._httpClient.SetBearerToken(this._accessToken);

            var httpResponse = await this._httpClient.SendAsync(request);

            if (!httpResponse.IsSuccessStatusCode)
            {
                //error
                return Result.Failure(new List<string>() { httpResponse.ReasonPhrase });
            }

            return true;
        }

        protected async Task<Result<TData>> MakeAuthenticatedRequest<TData>(HttpRequestMessage request, string accessToken)
        {
            this._accessToken = accessToken;
            return await this.MakeAuthenticatedRequest<TData>(request);
        }

        protected async Task<Result<TData>> MakeAuthenticatedRequest<TData>(HttpRequestMessage request)
        {
            this._httpClient.SetBearerToken(this._accessToken);

            var httpResponse =  await this._httpClient.SendAsync(request);

            if (!httpResponse.IsSuccessStatusCode)
            {
                //error
                return Result<TData>.Failure(new List<string>() { httpResponse.ReasonPhrase });
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<TData>(content, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return Result<TData>.SuccessWith(model);
        }
    }
}
