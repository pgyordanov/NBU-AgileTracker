namespace AgileTracker.Client.Startup.Controllers.Estimations
{
    using System.Text.Json;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstmationStatistics;
    using AgileTracker.Client.Startup.Infrastructure;
    using AgileTracker.Client.Startup.Models.Tasks.Estimations;

    using AutoMapper;

    using MediatR;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("statistics")]
    public class EstimationsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EstimationsController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "IsProjectGroupOwner")]
        [Route("project-group/{projectGroupId}")]
        public async Task<IActionResult> Get(int projectGroupId, int? projectId)
        {
            var estimationsCommand = new GetTaskEstimationStatisticsCommand(projectGroupId, projectId);
            var estimationsResult = await this._mediator.Send(estimationsCommand);

            if (!estimationsResult.Succeeded)
            {
                return new JsonResult(new { Succeeded = false, Errors = estimationsResult.Errors });
            }

            var estimationsModel = this._mapper.Map<GetTaskEstimationStatisticsOutputModel, GetStatisticsViewModel>(estimationsResult.Data);

            return new JsonResult(estimationsModel, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
