namespace AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstmationStatistics
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;

    using MediatR;

    public class GetTaskEstimationStatisticsCommand : GetTaskEstimationStatisticsInputModel, IRequest<Result<GetTaskEstimationStatisticsOutputModel>>
    {
        public GetTaskEstimationStatisticsCommand(int? projectGroupId, int? projectId) 
            : base(projectGroupId, projectId)
        {
        }

        public class GetTaskEstimationStatisticsCommandHandler : IRequestHandler<GetTaskEstimationStatisticsCommand, Result<GetTaskEstimationStatisticsOutputModel>>
        {
            private readonly IGatewayService _gatewayService;

            public GetTaskEstimationStatisticsCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<GetTaskEstimationStatisticsOutputModel>> Handle(GetTaskEstimationStatisticsCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.GetTaskEstimationStatistics(request);
            }
        }
    }
}
