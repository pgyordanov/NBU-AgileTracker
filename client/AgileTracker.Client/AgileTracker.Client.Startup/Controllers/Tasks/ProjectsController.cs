namespace AgileTracker.Client.Startup.Controllers.Tasks
{
    using AgileTracker.Client.Startup.Infrastructure;

    using AutoMapper;

    using MediatR;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("project-group/{projectGroupId}/project/{projectId}")]
    public class ProjectsController: BaseController
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
        public IActionResult Index(int projectGroupId, int projectId)
        {
            return View();
        }
    }
}
