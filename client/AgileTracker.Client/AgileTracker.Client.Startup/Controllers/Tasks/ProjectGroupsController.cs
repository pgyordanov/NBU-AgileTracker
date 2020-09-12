namespace AgileTracker.Client.Startup.Controllers.Tasks
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Features.Tasks.Commands.AcceptProjectGroupInvitation;
    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateProject;
    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Commands.InviteProjectGroupMember;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroupInvitations;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Client.Startup.Infrastructure;
    using AgileTracker.Client.Startup.Infrastructure.UI;
    using AgileTracker.Client.Startup.Models;
    using AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.CreateProject;
    using AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.CreateProjectGroup;
    using AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.Index;
    using AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.InvitationInbox;
    using AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.InviteProjectGroupMember;

    using AutoMapper;

    using MediatR;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("")]
    public class ProjectGroupsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectGroupsController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var command = new GetProjectGroupsCommand();
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            var model = this._mapper.ProjectTo<ProjectGroupViewModel>(result.Data.AsQueryable());

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
        [Route("invitations")]
        public async Task<IActionResult> InvitationInbox()
        {
            var command = new GetProjectGroupInvitationsCommand();
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            var model = this._mapper.ProjectTo<ProjectGroupInvitationViewModel>(result.Data.AsQueryable());

            return View(model);
        }

        [HttpGet]
        [Route("invitations/{projectGroupId}/accept")]
        public async Task<IActionResult> AcceptInvitation(int projectGroupId)
        {
            var command = new AcceptProjectGroupInvitationCommand(projectGroupId);
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            //sign out of the cookie scheme, so idsrv can be asked to authenticate user and project group claims can be populated. 
            //Idsrv keeps it's cookie, so user is not prompted to login manually
            await this.HttpContext.SignOutAsync();

            return RedirectToAction(nameof(this.Group), new { ProjectGroupId = projectGroupId });
        }

        [HttpGet]
        [Route("project-group/{projectGroupId}")]
        [Authorize(Policy = "IsProjectGroupMember")]
        public async Task<IActionResult> Group(int projectGroupId)
        {
            var command = new GetProjectGroupCommand(projectGroupId);

            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            var model =
                this._mapper.Map<GetProjectGroupOutputModel, Models.Tasks.ProjectGroups.Group.ProjectGroupViewModel>(result.Data);

            return View(model);
        }

        [HttpGet]
        [Route("project-group/{projectGroupId}/invite-member")]
        [Authorize(Policy = "IsProjectGroupOwner")]
        public IActionResult InviteProjectGroupMember()
            => View();

        [HttpPost]
        [Route("project-group/{projectGroupId}/invite-member")]
        [Authorize(Policy = "IsProjectGroupOwner")]
        public async Task<IActionResult> InviteProjectGroupMember(int projectGroupId, InviteProjectGroupMemberViewModel model)
        {
            var command = new InviteProjectGroupMemberCommand(projectGroupId, model.MemberEmailAddress);
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            return this.RedirectToAction(nameof(this.Group), new { ProjectGroupId = projectGroupId })
                .WithSuccess("Member invited", $"Successfully invited {model.MemberEmailAddress}");
        }

        [HttpGet]
        [Route("project-group/{projectGroupId}/project/create")]
        [Authorize(Policy = "IsProjectGroupOwner")]
        public IActionResult CreateProject()
           => View();

        [HttpPost]
        [Route("project-group/{projectGroupId}/project/create")]
        [Authorize(Policy = "IsProjectGroupOwner")]
        public async Task<IActionResult> CreateProject(int projectGroupId, CreateProjectViewModel model)
        {
            var command = new CreateProjectCommand(projectGroupId, model.Title);
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            return this.RedirectToAction(
                nameof(ProjectsController.Index),
                "Projects",
                new { ProjectGroupId = projectGroupId, ProjectId = result.Data.ProjectId });
        }


        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
