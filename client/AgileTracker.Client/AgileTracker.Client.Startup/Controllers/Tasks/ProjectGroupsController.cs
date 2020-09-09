namespace AgileTracker.Client.Startup.Controllers.Tasks
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using AgileTracker.Client.Application.Features.Tasks.Commands;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Client.Startup.Infrastructure;
    using AgileTracker.Client.Startup.Models;
    using AgileTracker.Client.Startup.Models.Tasks.CreateProjectGroup;
    using AgileTracker.Client.Startup.Models.Tasks.Index;

    using MediatR;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("")]
    public class ProjectGroupsController : BaseController
    {
        private readonly IMediator _mediator;

        public ProjectGroupsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var command = new GetProjectGroupsCommand();
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            var model = result.Data
                .Select(g => new ProjectGroupViewModel
                {
                    Id = g.Id,
                    GroupName = g.GroupName,
                    Members = g.Members.Select(m=> new ProjectGroupMemberViewModel
                    {
                        Id = m.Id,
                        IsOwner = m.IsOwner,
                        MemberId = m.MemberId,
                    }).ToList()
                });

            return View(model);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult CreateProjectGroup()
            => View();

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProjectGroup(CreateProjectGroupViewModel model)
        {
            var command = new CreateProjectGroupCommand(model.ProjectGroupName);
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            return this.RedirectToAction(nameof(this.Group), new { ProjectGroupId = result.Data.GroupId });
        }

        [HttpGet]
        [Route("project-group/{projectGroupId}")]
        [Authorize(Policy = "IsProjectGroupMember")]
        public IActionResult Group(int projectGroupId)
        {
            return View();
        }

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
