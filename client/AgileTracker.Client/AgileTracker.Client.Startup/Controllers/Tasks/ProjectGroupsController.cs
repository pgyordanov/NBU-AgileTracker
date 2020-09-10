namespace AgileTracker.Client.Startup.Controllers.Tasks
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Features.Tasks.Commands.AcceptProjectGroupInvitation;
    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Commands.InviteProjectGroupMember;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroupInvitations;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Client.Startup.Infrastructure;
    using AgileTracker.Client.Startup.Infrastructure.UI;
    using AgileTracker.Client.Startup.Models;
    using AgileTracker.Client.Startup.Models.Tasks.CreateProjectGroup;
    using AgileTracker.Client.Startup.Models.Tasks.Index;
    using AgileTracker.Client.Startup.Models.Tasks.InvitationInbox;
    using AgileTracker.Client.Startup.Models.Tasks.InviteProjectGroupMember;

    using MediatR;

    using Microsoft.AspNetCore.Authentication;
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
                    Members = g.Members.Select(m => new ProjectGroupMemberViewModel
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
        [Route("invitations")]
        public async Task<IActionResult> InvitationInbox()
        {
            var command = new GetProjectGroupInvitationsCommand();
            var result = await this._mediator.Send(command);

            var actionResult = this.HandleResultValidation(result);

            if (actionResult != null)
                return actionResult;

            var model = result.Data.Select(i => new ProjectGroupInvitationViewModel
            {
                GroupId = i.GroupId,
                GroupName = i.GroupName
            });

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

            var group = result.Data;
            var model = new Models.Tasks.Group.ProjectGroupViewModel
            {
                Id = group.Id,
                GroupName = group.GroupName,
                Members = group.Members.Select(m => new Models.Tasks.Group.ProjectGroupMemberViewModel
                {
                    Id = m.Id,
                    MemberId = m.MemberId,
                    UserName = m.UserName,
                    Firstname = m.Firstname,
                    Lastname = m.Lastname,
                    IsOwner = m.IsOwner
                })
            };
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

            return this.RedirectToAction(nameof(this.Group), new { ProjectGroupId = projectGroupId }).WithSuccess("Member invited", $"Successfully invited {model.MemberEmailAddress}");
        }

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
