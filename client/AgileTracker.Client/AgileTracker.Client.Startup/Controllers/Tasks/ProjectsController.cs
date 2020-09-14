namespace AgileTracker.Client.Startup.Controllers.Tasks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Features.Tasks.Commands.AddToProjectBacklog;
    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateSprint;
    using AgileTracker.Client.Application.Features.Tasks.Commands.FinishSprint;
    using AgileTracker.Client.Application.Features.Tasks.Commands.RemoveFromProjectBacklog;
    using AgileTracker.Client.Application.Features.Tasks.Commands.RemoveProject;
    using AgileTracker.Client.Application.Features.Tasks.Commands.RemoveSprint;
    using AgileTracker.Client.Application.Features.Tasks.Commands.UpdateBacklogTask;
    using AgileTracker.Client.Application.Features.Tasks.Commands.UpdateSprintTaskStatus;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint;
    using AgileTracker.Client.Startup.Infrastructure;
    using AgileTracker.Client.Startup.Infrastructure.UI;
    using AgileTracker.Client.Startup.Models.Tasks.Projects.AddToBacklog;
    using AgileTracker.Client.Startup.Models.Tasks.Projects.CreateSprint;
    using AgileTracker.Client.Startup.Models.Tasks.Projects.Index;
    using AgileTracker.Client.Startup.Models.Tasks.Projects.Sprint;
    using AgileTracker.Client.Startup.Models.Tasks.Projects.UpdateTaskStatus;

    using AutoMapper;

    using MediatR;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("project-group/{projectGroupId}/project/{projectId}")]
    public class ProjectsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectsController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "IsProjectGroupMember")]
        [Route("")]
        public async Task<IActionResult> Index(int projectGroupId, int projectId)
        {
            var command = new GetProjectCommand(projectGroupId, projectId);
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            var model = this._mapper.Map<GetProjectOutputModel, GetProjectViewModel>(result.Data);
            model.ProjectGroupId = projectGroupId;

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "IsProjectGroupOwner")]
        [Route("remove")]
        public async Task<IActionResult> RemoveProject(int projectGroupId, int projectId)
        {
            var command = new RemoveProjectCommand(projectGroupId, projectId);

            var result = await this._mediator.Send(command);

            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId })
                    .WithDanger("An error has occured", string.Join("\n ", result.Errors));
            }

            return RedirectToAction(nameof(ProjectGroupsController.Group), "ProjectGroups", new { ProjectGroupId = projectGroupId });
        }

        [HttpPost]
        [Authorize(Policy = "IsProjectGroupMember")]
        [Route("add-to-backlog")]
        public async Task<IActionResult> AddToBacklog(int projectGroupId, int projectId, AddTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId })
                    .WithDanger("Could not add task", "An error has occured while adding the task");
            }

            var command =
                new AddToProjectBacklogCommand(projectGroupId, projectId, model.Title, model.Description, model.PointsEstimate, model.AssignedToMemberId, DateTime.Now);

            var result = await this._mediator.Send(command);

            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId })
                    .WithDanger("An error has occured", string.Join("\n ", result.Errors));
            }

            return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId });
        }

        [HttpPost]
        [Authorize(Policy = "IsProjectGroupMember")]
        [Route("remove-from-backlog/{taskId}")]
        public async Task<IActionResult> RemoveFromBacklog(int projectGroupId, int projectId, int taskId)
        {
            var command = new RemoveFromProjectBacklogCommand(projectGroupId, projectId, taskId);
            var result = await this._mediator.Send(command);

            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId })
                    .WithDanger("An error has occured", string.Join("\n ", result.Errors));
            }

            return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId });
        }

        [HttpPost]
        [Authorize(Policy = "IsProjectGroupMember")]
        [Route("update-backlog-task/{taskId}")]
        public async Task<IActionResult> UpdateBacklogTask(int projectGroupId, int projectId, int taskId, AddTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId })
                    .WithDanger("Could not update task", "An error has occured while updating the task");
            }

            var command =
                new UpdateBacklogTaskCommand(projectGroupId, projectId, taskId, model.Title, model.Description, model.PointsEstimate, model.AssignedToMemberId);

            var result = await this._mediator.Send(command);

            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId })
                    .WithDanger("An error has occured", string.Join("\n ", result.Errors));
            }

            return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId });
        }

        [HttpPost]
        [Authorize(Policy = "IsProjectGroupOwner")]
        [Route("create-sprint")]
        public async Task<IActionResult> CreateSprint(int projectGroupId, int projectId, CreateSprintViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId })
                    .WithDanger("Could not create sprint", "An error has occured while creating the sprint");
            }

            var command = new CreateSprintCommand(projectGroupId, projectId, model.DurationWeeks, model.SprintBacklog, DateTime.Now);
            var result = await this._mediator.Send(command);

            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId })
                    .WithDanger("An error has occured", string.Join("\n ", result.Errors));
            }

            return RedirectToAction(nameof(this.Sprint), new { ProjectGroupId = projectGroupId, ProjectId = projectId, SprintId = result.Data.SprintId });
        }

        [HttpGet]
        [Authorize(Policy = "IsProjectGroupMember")]
        [Route("sprint/{sprintId}")]
        public async Task<IActionResult> Sprint(int projectGroupId, int projectId, int sprintId)
        {
            var command = new GetSprintCommand(projectGroupId, projectId, sprintId);
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            var model = this._mapper.Map<GetSprintOutputModel, GetSprintViewModel>(result.Data);
            model.ProjectGroupId = projectGroupId;
            model.ProjectId = projectId;

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "IsProjectGroupMember")]
        [Route("sprint/{sprintId}/update-task-status/{taskId}")]
        public async Task<IActionResult> UpdateSprintTaskStatus(int projectGroupId, int projectId, int sprintId, int taskId, UpdateTaskStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Sprint), new { ProjectGroupId = projectGroupId, ProjectId = projectId, SprintId = sprintId })
                    .WithDanger("Could not update task", "An error has occured while updating the task");
            }

            var command =
                new UpdateSprintTaskStatusCommand(projectGroupId, projectId, sprintId, taskId, model.TaskStatus);

            var result = await this._mediator.Send(command);

            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(this.Sprint), new { ProjectGroupId = projectGroupId, ProjectId = projectId, SprintId = sprintId })
                    .WithDanger("An error has occured", string.Join("\n ", result.Errors));
            }

            return RedirectToAction(nameof(this.Sprint), new { ProjectGroupId = projectGroupId, ProjectId = projectId, SprintId = sprintId });
        }

        [HttpPost]
        [Authorize(Policy = "IsProjectGroupOwner")]
        [Route("sprint/{sprintId}/finish")]
        public async Task<IActionResult> FinishSprint(int projectGroupId, int projectId, int sprintId)
        {
            var command = new FinishSprintCommand(projectGroupId, projectId, sprintId);

            var result = await this._mediator.Send(command);

            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(this.Sprint), new { ProjectGroupId = projectGroupId, ProjectId = projectId, SprintId = sprintId })
                    .WithDanger("An error has occured", string.Join("\n ", result.Errors));
            }

            return RedirectToAction(nameof(this.Sprint), new { ProjectGroupId = projectGroupId, ProjectId = projectId, SprintId = sprintId });
        }

        [HttpPost]
        [Authorize(Policy = "IsProjectGroupOwner")]
        [Route("sprint/{sprintId}/remove")]
        public async Task<IActionResult> RemoveSprint(int projectGroupId, int projectId, int sprintId)
        {
            var command = new RemoveSprintCommand(projectGroupId, projectId, sprintId);

            var result = await this._mediator.Send(command);

            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(this.Sprint), new { ProjectGroupId = projectGroupId, ProjectId = projectId, SprintId = sprintId })
                    .WithDanger("An error has occured", string.Join("\n ", result.Errors));
            }

            return RedirectToAction(nameof(this.Index), new { ProjectGroupId = projectGroupId, ProjectId = projectId });
        }
    }
}
