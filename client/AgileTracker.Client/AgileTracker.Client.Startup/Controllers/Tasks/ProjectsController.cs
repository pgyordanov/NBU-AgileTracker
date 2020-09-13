namespace AgileTracker.Client.Startup.Controllers.Tasks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Features.Tasks.Commands.AddToProjectBacklog;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Client.Startup.Infrastructure;
    using AgileTracker.Client.Startup.Infrastructure.UI;
    using AgileTracker.Client.Startup.Models.Tasks.Projects.AddToBacklog;
    using AgileTracker.Client.Startup.Models.Tasks.Projects.Index;

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
    }
}
