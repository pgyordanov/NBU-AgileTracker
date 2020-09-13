namespace AgileTracker.TasksService.Web.Features
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AgileTracker.TasksService.Application.Features.Commands.AddMemberToProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.AddToProjectBacklog;
    using AgileTracker.TasksService.Application.Features.Commands.CreateProject;
    using AgileTracker.TasksService.Application.Features.Commands.CreateProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.CreateSprint;
    using AgileTracker.TasksService.Application.Features.Commands.FinishSprint;
    using AgileTracker.TasksService.Application.Features.Commands.InviteMemberToProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.RemoveFromProjectBacklog;
    using AgileTracker.TasksService.Application.Features.Commands.RemoveProject;
    using AgileTracker.TasksService.Application.Features.Commands.RemoveProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.RemoveSprint;
    using AgileTracker.TasksService.Application.Features.Commands.UpdateBacklogTask;
    using AgileTracker.TasksService.Application.Features.Commands.UpdateSprintTaskStatus;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProject;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroupInvitations;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberSprint;
    using AgileTracker.TasksService.Web.Common;

    using MediatR;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Swashbuckle.AspNetCore.Filters;

    [ApiController]
    [Route("api/[Controller]")]
    public class ProjectGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectGroupsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Attempts to create a project group with provided group name and owner ID
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">If the project group is created successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateProjectGroupOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(CreateProjectGroupCommand), typeof(ProjectGroupsSwaggerExamples.CreateProjectGroupExample))]
        public async Task<ActionResult<CreateProjectGroupOutputModel>> CreateProjectGroup(CreateProjectGroupCommand command)
        {
            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to remove a project group with the provided project group id and member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the project group is removed successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("remove-project-group/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(RemoveProjectGroupCommand), typeof(ProjectGroupsSwaggerExamples.RemoveProjectGroupExample))]
        public async Task<ActionResult> RemoveProjectGroup([FromRoute] string memberId, [FromBody] RemoveProjectGroupCommand command)
        {
            command = new RemoveProjectGroupCommand(command.ProjectGroupId, memberId);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to invite a member to the project group with the provided group ID, owner ID and member to be added ID
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the member is invited successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("invite-group-member/{ownerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(
            typeof(InviteMemberToProjectGroupCommand),
            typeof(ProjectGroupsSwaggerExamples.InviteMemberToProjectGroupExample))]
        public async Task<ActionResult> InviteProjectGroupMember(string ownerId, InviteMemberToProjectGroupCommand command)
        {
            //ownerId as a path parameter is a workaround; send headers from gateway with sub claim

            command = new InviteMemberToProjectGroupCommand(command.ProjectGroupId, ownerId, command.MemberId);
            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to add a member to the project group with the provided group ID and member to be added ID
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">If the member is added successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("add-group-member")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(
            typeof(AddMemberToProjectGroupCommand),
            typeof(ProjectGroupsSwaggerExamples.AddMemberToProjectGroupExample))]
        public async Task<ActionResult> AddMemberToProjectGroup(AddMemberToProjectGroupCommand command)
        {
            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to fetch all project groups a member with ID is apart of
        /// </summary>
        /// <param name="memberId"></param>
        /// <response code="200">If the project groups for member are fetched successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpGet]
        [Route("get-member-groups/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GetMemberProjectGroupsOutputModel>>> GetMemberProjectGroups([FromRoute] string memberId)
        {
            GetMemberProjectGroupsCommand command = new GetMemberProjectGroupsCommand(memberId);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to fetch all project groups a member with ID is invited to
        /// </summary>
        /// <param name="memberId"></param>
        /// <response code="200">If the project groups for member are fetched successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpGet]
        [Route("get-member-group-invitations/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GetMemberProjectGroupInvitationsOutputModel>>> GetMemberProjectGroupInvitations(
            [FromRoute] string memberId)
        {
            GetMemberProjectGroupInvitationsCommand command = new GetMemberProjectGroupInvitationsCommand(memberId);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts create a project for the provided project group
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the project groups for member are fetched successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("create-project/{ownerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(
            typeof(CreateProjectCommand),
            typeof(ProjectGroupsSwaggerExamples.CreateProjectExample))]
        public async Task<ActionResult<CreateProjectOutputModel>> CreateProject(
            [FromRoute] string ownerId,
            [FromBody] CreateProjectCommand command)
        {
            command = new CreateProjectCommand(command.ProjectGroupId, ownerId, command.Title);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to fetch a project with the provided project group id, project id and member id
        /// </summary>
        /// <param name="projectGroupId"></param>
        /// <param name="projectId"></param>
        /// <param name="memberId"></param>
        /// <response code="200">If the project for this member is fetched successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpGet]
        [Route("{projectGroupId}/project/{projectId}/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetMemberProjectOutputModel>> GetProject(
            [FromRoute] int projectGroupId,
            [FromRoute] int projectId,
            [FromRoute] string memberId)
        {
            var command = new GetMemberProjectCommand(projectGroupId, projectId, memberId);
            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to add to the project backlog with the provided project group id, project id and member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the task is added successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("add-to-backlog/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(
            typeof(AddToProjectBacklogCommand),
            typeof(ProjectGroupsSwaggerExamples.AddToProjectBacklogExample))]
        public async Task<ActionResult> AddToProjectBacklog(
            [FromRoute] string memberId,
            [FromBody] AddToProjectBacklogCommand command)
        {
            command = new AddToProjectBacklogCommand(
                                                    command.ProjectGroupId,
                                                    command.ProjectId,
                                                    memberId,
                                                    command.Title,
                                                    command.Description,
                                                    command.PointsEstimate,
                                                    command.AssignedToMemberId,
                                                    command.StartsOn);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to remove from the project backlog with the provided project group id, project id, task id and member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the task is removed successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("remove-from-backlog/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(
            typeof(RemoveFromProjectBacklogCommand),
            typeof(ProjectGroupsSwaggerExamples.RemoveFromProjectBacklogExample))]
        public async Task<ActionResult> RemoveFromProjectBacklog(
            [FromRoute] string memberId,
            [FromBody] RemoveFromProjectBacklogCommand command)
        {
            command = new RemoveFromProjectBacklogCommand(
                                                    command.ProjectGroupId,
                                                    command.ProjectId,
                                                    memberId,
                                                    command.TaskId);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to update a backlog task with the provided project group id, project id, task id and member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the task is updated successfully successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("update-backlog-task/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(
            typeof(UpdateBacklogTaskCommand),
            typeof(ProjectGroupsSwaggerExamples.UpdateBacklogTaskExample))]
        public async Task<ActionResult> UpdateBacklogTask(
            [FromRoute] string memberId,
            [FromBody] UpdateBacklogTaskCommand command)
        {
            command = new UpdateBacklogTaskCommand(
                                                    command.ProjectGroupId,
                                                    command.ProjectId,
                                                    command.TaskId,
                                                    memberId,
                                                    command.Title,
                                                    command.Description,
                                                    command.PointsEstimate,
                                                    command.AssignedToMemberId);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to remove a project with the provided project group id, project id and member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the project is removed successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("remove-project/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(RemoveProjectCommand), typeof(ProjectGroupsSwaggerExamples.RemoveProjectExample))]
        public async Task<ActionResult> RemoveProject([FromRoute] string memberId, [FromBody] RemoveProjectCommand command)
        {
            command = new RemoveProjectCommand(command.ProjectGroupId, command.ProjectId, memberId);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to create a sprint for the project with the provided project group id, project id and member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the sprint is created successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("create-sprint/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(
            typeof(CreateSprintCommand),
            typeof(ProjectGroupsSwaggerExamples.CreateSprintExample))]
        public async Task<ActionResult<CreateSprintOutputModel>> CreateSprint(
            [FromRoute] string memberId,
            [FromBody] CreateSprintCommand command)
        {
            command = new CreateSprintCommand(command.ProjectGroupId, command.ProjectId, memberId, command.TaskIds, command.StartsOn, command.DurationWeeks);
            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to fetch a sprint with the provided project group id, project id, sprint id and member id
        /// </summary>
        /// <param name="projectGroupId"></param>
        /// <param name="projectId"></param>
        /// <param name="sprintId"></param>
        /// <param name="memberId"></param>
        /// <response code="200">If the sprint for this member is fetched successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpGet]
        [Route("{projectGroupId}/project/{projectId}/sprint/{sprintId}/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetMemberSprintOutputModel>> GetSprint(
            [FromRoute] int projectGroupId,
            [FromRoute] int projectId,
            [FromRoute] int sprintId,
            [FromRoute] string memberId)
        {
            var command = new GetMemberSprintCommand(projectGroupId, projectId, sprintId, memberId);
            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to update a sprint task status with the provided project group id, project id, sprint id, task id and member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the task status is updated successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("update-sprint-task-status/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(
            typeof(UpdateSprintTaskStatusCommand),
            typeof(ProjectGroupsSwaggerExamples.UpdateSprintTaskStatusExample))]
        public async Task<ActionResult> UpdateSprintTaskStatus([FromRoute] string memberId,
            [FromBody] UpdateSprintTaskStatusCommand command)
        {
            command = new UpdateSprintTaskStatusCommand(
                                                    command.ProjectGroupId,
                                                    command.ProjectId,
                                                    command.TaskId,
                                                    command.SprintId,
                                                    memberId,
                                                    command.TaskStatus);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to finish a sprint with the provided project group id, project id, sprint id, and member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the sprint is finished successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("finish-sprint/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(FinishSprintCommand), typeof(ProjectGroupsSwaggerExamples.FinishSprintExample))]
        public async Task<ActionResult> FinishSprint([FromRoute] string memberId, [FromBody] FinishSprintCommand command)
        {
            command = new FinishSprintCommand(command.ProjectGroupId, command.ProjectId, command.SprintId, memberId);

            return await this._mediator.Send(command).ToActionResult();
        }

        /// <summary>
        /// Attempts to remove a sprint with the provided project group id, project id, sprint id, and member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="command"></param>
        /// <response code="200">If the sprint is removed successfully</response>
        /// <response code="400">If there was an encountered error with processing the request</response>
        [HttpPost]
        [Route("remove-sprint/{memberId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(RemoveSprintCommand), typeof(ProjectGroupsSwaggerExamples.RemoveSprintExample))]
        public async Task<ActionResult> RemoveSprint([FromRoute] string memberId, [FromBody] RemoveSprintCommand command)
        {
            command = new RemoveSprintCommand(command.ProjectGroupId, command.ProjectId, command.SprintId, memberId);

            return await this._mediator.Send(command).ToActionResult();
        }
    }
}
