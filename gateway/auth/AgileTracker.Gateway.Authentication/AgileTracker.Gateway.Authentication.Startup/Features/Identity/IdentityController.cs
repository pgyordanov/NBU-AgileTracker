namespace AgileTracker.Gateway.Authentication.IdentityServer.Features.Identity
{
    using System;
    using System.Threading.Tasks;

    using AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LoginUser;
    using AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LogoutUser;
    using AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.RegisterUser;
    using AgileTracker.Gateway.Authentication.Startup.Common;
    using AgileTracker.Gateway.Authentication.Startup.Features.Identity.Login;
    using AgileTracker.Gateway.Authentication.Startup.Features.Identity.Register;

    using IdentityServer4.Extensions;
    using IdentityServer4.Services;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;

    [Route("auth")]
    public class IdentityController: Controller
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
            {
                throw new InvalidOperationException($"{nameof(returnUrl)} is required");
            }

            if (this.User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }

            return View(new LoginViewModel { ReturnUrl = returnUrl});
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var command = new LoginUserCommand(model.EmailAddress, model.Password, model.RememberMe);

            return await this.ExecuteCommand(this._mediator.Send(command), model, Redirect(model.ReturnUrl));
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
            {
                throw new InvalidOperationException($"{nameof(returnUrl)} is required");
            }

            if (this.User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }

            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var command = new RegisterUserCommand(model.Firstname, model.Lastname, model.EmailAddress, model.Password);

            return await this.ExecuteCommand(this._mediator.Send(command), model, Redirect(returnUrl));
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            if (logoutId.IsNullOrEmpty())
            {
                throw new InvalidOperationException($"{nameof(logoutId)} is required");
            }

            var command = new LogoutUserCommand(logoutId);

            var result = await this._mediator.Send(command);

            return Redirect(result.Data.PostLogoutRedirectUri);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
