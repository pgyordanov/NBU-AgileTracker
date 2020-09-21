namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetTaskEstimations
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Contracts;
    using AgileTracker.Common.Application;
    using MediatR;

    public class GetTaskEstimationsCommand : GetTaskEstimationsInputModel, IRequest<Result<IEnumerable<GetTaskEstimationsOutputModel>>>
    {
        public GetTaskEstimationsCommand(int? projectGroupId, int? projectId, int? taskId, bool? onlyCompleted) : base(projectGroupId, projectId, taskId, onlyCompleted)
        {
        }

        public class GetTaskEstimationsCommandHandler : IRequestHandler<GetTaskEstimationsCommand, Result<IEnumerable<GetTaskEstimationsOutputModel>>>
        {
            private readonly IGatewayService _gatewayService;

            public GetTaskEstimationsCommandHandler(IGatewayService gatewayService)
            {
                this._gatewayService = gatewayService;
            }

            public async Task<Result<IEnumerable<GetTaskEstimationsOutputModel>>> Handle(GetTaskEstimationsCommand request, CancellationToken cancellationToken)
            {
                return await this._gatewayService.GetTaskEstimations(request);
            }
        }
    }
}
