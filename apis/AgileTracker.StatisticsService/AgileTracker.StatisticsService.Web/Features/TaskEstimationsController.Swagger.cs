namespace AgileTracker.StatisticsService.Web.Features
{
    using System;

    using AgileTracker.StatisticsService.Application.Features.Commands.CreateEstimation;

    using Swashbuckle.AspNetCore.Filters;

    public class TaskEstimationsSwaggerExamples
    {
        public class CreateEstimationExample : IExamplesProvider<CreateEstimationCommand>
        {
            public CreateEstimationCommand GetExamples()
                => new CreateEstimationCommand(1, 1, 1, "c210a166-28a6-4417-b71a-6ca777a0f493", DateTime.Now, DateTime.Now.AddDays(5));
        }
    }
}
