namespace AgileTracker.Client.Startup.Infrastructure
{
    using AgileTracker.Common.Application;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController: Controller
    {
        protected IActionResult HandleResultValidation(Result result)
        {
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error);
                }

                return View();
            }

            return null;
        }
    }
}
