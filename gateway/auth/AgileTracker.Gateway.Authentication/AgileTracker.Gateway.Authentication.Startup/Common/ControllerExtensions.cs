namespace AgileTracker.Gateway.Authentication.Startup.Common
{
    using System;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;

    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static async Task<IActionResult> ExecuteCommand(
            this Controller controller,
            Task<Result> resultTask,
            object viewModel,
            IActionResult successActionResult)
        {
            var result = await resultTask;

            if (result.Succeeded)
            {
                return successActionResult;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    controller.ModelState.AddModelError(error, error);
                }

                return controller.View(viewModel);
            }
        }

        public static async Task<IActionResult> ExecuteCommand<TData>(
            this Controller controller,
            Task<Result<TData>> resultTask,
            object viewModel,
            IActionResult successActionResult)
        {
            var result = await resultTask;

            if (result.Succeeded)
            {
                return successActionResult;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    controller.ModelState.AddModelError(error, error);
                }

                return controller.View(viewModel);
            }
        }
    }

}
