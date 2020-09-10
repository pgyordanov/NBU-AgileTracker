namespace AgileTracker.Client.Startup.Infrastructure.UI
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class AlertDecoratorActionResult : IActionResult
    {
        public AlertDecoratorActionResult(IActionResult result, string type, string title, string body)
        {
            Result = result;
            Type = type;
            Title = title;
            Body = body;
        }

        public IActionResult Result { get; }

        public string Type { get; }

        public string Title { get; }

        public string Body { get; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var tempDataFactory = (ITempDataDictionaryFactory)context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory));

            var tempDataDictionary = tempDataFactory.GetTempData(context.HttpContext);

            tempDataDictionary["_alert.type"] = Type;
            tempDataDictionary["_alert.title"] = Title;
            tempDataDictionary["_alert.body"] = Body;

            await Result.ExecuteResultAsync(context);
        }
    }
}
