namespace AgileTracker.StatisticsService.Web.Features
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AgileTracker.StatisticsService.Application.Features.Commands.CreateEstimation;
    using AgileTracker.StatisticsService.Application.Features.Queries.GetTaskEstimations;
    using AgileTracker.StatisticsService.Web.Common;

    using MediatR;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Swashbuckle.AspNetCore.Filters;

    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class TaskEstimationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskEstimationsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Attempts to fetch all task estimations with the provided parameters
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="projectGroupId"></param>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <param name="onlyCompleted"></param>
        /// <response code="200">If the task estimations are fetched successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpGet]
        [Route("{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GetTaskEstimationsOutputModel>>> GetEstimations(
            [FromRoute] string memberId,
            [FromQuery] int? projectGroupId,
            [FromQuery] int? projectId,
            [FromQuery] int? taskId,
            [FromQuery] bool? onlyCompleted)
        {
            var command = new GetTaskEstimationsCommand(projectGroupId, projectId, taskId, onlyCompleted, memberId);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to add a task finish estimation with the provided parameters
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the task estimation is added successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(CreateEstimationCommand), typeof(TaskEstimationsSwaggerExamples.CreateEstimationExample))]
        public async Task<ActionResult> CreateEstimation([FromRoute] string memberId, [FromBody] CreateEstimationCommand command)
        {
            command = new CreateEstimationCommand(
                command.ProjectGroupId,
                command.ProjectId,
                command.TaskId,
                memberId,
                command.StartedOn,
                command.EstimatedToFinishOn);

            return await this._mediator.Send(command).ToActionResult();
        }
    }
}
