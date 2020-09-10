namespace AgileTracker.Gateway.Authentication.Startup.Features.IdentityApi
{
    using System.Threading.Tasks;

    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.GetUsersInformation;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.IsEmailRegistered;
    using AgileTracker.Gateway.Authentication.Startup.Common;

    using MediatR;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static IdentityServer4.IdentityServerConstants;

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class IdentityApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityApiController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<GetUsersInformationOutputModel>> GetUsersInformation([FromBody]GetUsersInformationCommand command)
        {
            return await this._mediator.Send(command).ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<IsEmailRegisteredOutputModel>> IsEmailRegistered([FromBody]IsEmailRegisteredCommand command)
        {
            return await this._mediator.Send(command).ToActionResult();
        }
    }
}
