﻿namespace AgileTracker.Client.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using AgileTracker.Client.Application.Configuration;
    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Client.Application.Features.Identity.GetUserInfo;
    using AgileTracker.Client.Application.Features.Identity.IsEmailRegistered;
    using AgileTracker.Client.Application.Features.Statistics.Commands.CreateTaskEstimation;
    using AgileTracker.Client.Application.Features.Statistics.Commands.UpdateTaskEstimation;
    using AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstimations;
    using AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstmationStatistics;
    using AgileTracker.Client.Application.Features.Tasks.Commands.AcceptProjectGroupInvitation;
    using AgileTracker.Client.Application.Features.Tasks.Commands.AddToProjectBacklog;
    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateProject;
    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateSprint;
    using AgileTracker.Client.Application.Features.Tasks.Commands.FinishSprint;
    using AgileTracker.Client.Application.Features.Tasks.Commands.RemoveFromProjectBacklog;
    using AgileTracker.Client.Application.Features.Tasks.Commands.RemoveProject;
    using AgileTracker.Client.Application.Features.Tasks.Commands.RemoveProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Commands.RemoveSprint;
    using AgileTracker.Client.Application.Features.Tasks.Commands.UpdateBacklogTask;
    using AgileTracker.Client.Application.Features.Tasks.Commands.UpdateSprintTaskStatus;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroupInvitations;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint;
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

        public async Task<Result<GetUserInfoOutputModel>> GetUserInfo(GetUserInfoInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.GetUserInfoEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                    JsonConvert.SerializeObject(new { userIds = input.UserIds }),
                    Encoding.UTF8,
                    "application/json")
            };

            return await this.MakeAuthenticatedRequest<GetUserInfoOutputModel>(request);
        }

        public async Task<Result<IsEmailRegisteredOutputModel>> IsEmailRegistered(IsEmailRegisteredInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.IsEmailRegisteredEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                    JsonConvert.SerializeObject(input),
                    Encoding.UTF8,
                    "application/json")
            };

            return await this.MakeAuthenticatedRequest<IsEmailRegisteredOutputModel>(request);
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

        public async Task<Result> RemoveProjectGroup(RemoveProjectGroupInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.RemoveProjectGroupEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
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

        public async Task<Result> InviteMemberToProjectGroup(int projectGroupId, string memberId)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.InviteMemberToProjectGroupEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                    JsonConvert.SerializeObject(new { projectGroupId, memberId }),
                    Encoding.UTF8,
                    "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result<IEnumerable<GetProjectGroupInvitationsOutputModel>>> GetProjectGroupInvitations()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.GetInvitationsForProjectGroupsForMemberEndpoint),
                Method = HttpMethod.Get
            };

            return await this.MakeAuthenticatedRequest<IEnumerable<GetProjectGroupInvitationsOutputModel>>(request);
        }

        public async Task<Result> AcceptProjectGroupInvitation(AcceptProjectGroupInvitationInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.AcceptProjectGroupInvitationEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                    JsonConvert.SerializeObject(new { projectGroupId = input.GroupId, memberId = this._currentUserService.UserId }),
                    Encoding.UTF8,
                    "application/json")
            };

            return await this.MakeAuthenticatedRequest<Result>(request);
        }

        public async Task<Result<CreateProjectOutputModel>> CreateProject(CreateProjectInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.CreateProjectEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                    JsonConvert.SerializeObject(new { projectGroupId = input.ProjectGroupId, title = input.ProjectTitle }),
                    Encoding.UTF8,
                    "application/json")
            };

            return await this.MakeAuthenticatedRequest<CreateProjectOutputModel>(request);
        }

        public async Task<Result> RemoveProject(RemoveProjectInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.RemoveProjectEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result<GetProjectOutputModel>> GetProject(GetProjectInputModel input)
        {
            var uriBuilder = new UriBuilder(this._gatewaySettings.BaseAddress + this._gatewaySettings.GetProjectEndpoint);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["projectGroupId"] = input.ProjectGroupId.ToString();
            query["projectId"] = input.ProjectId.ToString();
            uriBuilder.Query = query.ToString();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriBuilder.ToString()),
                Method = HttpMethod.Get
            };

            return await this.MakeAuthenticatedRequest<GetProjectOutputModel>(request);
        }

        public async Task<Result> AddToProjectBacklog(AddToProjectBacklogInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.AddToBacklogEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result> RemoveFromProjectBacklog(RemoveFromProjectBacklogInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.RemoveFromBacklogEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result> UpdateBacklogTask(UpdateBacklogTaskInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.UpdateBacklogTaskEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result<CreateSprintOutputModel>> CreateSprint(CreateSprintInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.CreateSprintEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                  JsonConvert.SerializeObject(input),
                  Encoding.UTF8,
                  "application/json")
            };

            return await this.MakeAuthenticatedRequest<CreateSprintOutputModel>(request);
        }

        public async Task<Result<GetSprintOutputModel>> GetSprint(GetSprintInputModel input)
        {
            var uriBuilder = new UriBuilder(this._gatewaySettings.BaseAddress + this._gatewaySettings.GetSprintEndpoint);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["projectGroupId"] = input.ProjectGroupId.ToString();
            query["projectId"] = input.ProjectId.ToString();
            query["sprintId"] = input.SprintId.ToString();
            uriBuilder.Query = query.ToString();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriBuilder.ToString()),
                Method = HttpMethod.Get
            };

            return await this.MakeAuthenticatedRequest<GetSprintOutputModel>(request);
        }

        public async Task<Result> UpdateSprintTaskStatus(UpdateSprintTaskStatusInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.UpdateSprintTaskStatusEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result> FinishSprint(FinishSprintInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.FinishSprintEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result> RemoveSprint(RemoveSprintInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.RemoveSprintEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result> CreateTaskEstimation(CreateTaskEstimationInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.CreateTaskEstimationEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result> UpdateTaskEstimation(UpdateTaskEstimationInputModel input)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this._gatewaySettings.BaseAddress + this._gatewaySettings.UpdateTaskEstimationEndpoint),
                Method = HttpMethod.Post,
                Content = new StringContent(
                   JsonConvert.SerializeObject(input),
                   Encoding.UTF8,
                   "application/json")
            };

            return await this.MakeAuthenticatedRequest(request);
        }

        public async Task<Result<IEnumerable<GetTaskEstimationsOutputModel>>> GetTaskEstimations(GetTaskEstimationsInputModel input)
        {
            var uriBuilder = new UriBuilder(this._gatewaySettings.BaseAddress + this._gatewaySettings.GetTaskEstimationsEndpoint);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["projectGroupId"] = input.ProjectGroupId?.ToString();
            query["projectId"] = input.ProjectId?.ToString();
            query["taskId"] = input.TaskId?.ToString();
            query["onlyCompleted"] = input.OnlyCompleted?.ToString();
            uriBuilder.Query = query.ToString();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriBuilder.ToString()),
                Method = HttpMethod.Get
            };

            return await this.MakeAuthenticatedRequest<IEnumerable<GetTaskEstimationsOutputModel>>(request);
        }

        public async Task<Result<GetTaskEstimationStatisticsOutputModel>> GetTaskEstimationStatistics(GetTaskEstimationStatisticsInputModel input)
        {
            var uriBuilder = new UriBuilder(this._gatewaySettings.BaseAddress + this._gatewaySettings.GetTaskEstimationStatisticsEndpoint);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["projectGroupId"] = input.ProjectGroupId?.ToString();
            query["projectId"] = input.ProjectId?.ToString();
            uriBuilder.Query = query.ToString();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriBuilder.ToString()),
                Method = HttpMethod.Get
            };

            return await this.MakeAuthenticatedRequest<GetTaskEstimationStatisticsOutputModel>(request);
        }
    }
}
