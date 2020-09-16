﻿namespace AgileTracker.TasksService.Application.Features.Commands.CreateProjectGroup
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.TasksService.Application.Contracts;
    using AgileTracker.TasksService.Application.Events.External;
    using AgileTracker.TasksService.Domain.Factories;

    using MediatR;

    public class CreateProjectGroupCommand : CreateProjectGroupInputModel, IRequest<Result<CreateProjectGroupOutputModel>>
    {
        public CreateProjectGroupCommand(string groupName, string ownerId)
            : base(groupName, ownerId)
        {
        }

        public class CreateProjectGroupCommandHandler : IRequestHandler<CreateProjectGroupCommand, Result<CreateProjectGroupOutputModel>>
        {
            private readonly IProjectGroupFactory _projectGroupFactory;
            private readonly IProjectGroupRepository _projectGroupRepository;
            private readonly IPublishExternalEvent _publishExternalEventService;

            public CreateProjectGroupCommandHandler(
                IProjectGroupFactory projectGroupFactory, 
                IProjectGroupRepository projectGroupRepository,
                IPublishExternalEvent publishExternalEventService)
            {
                this._projectGroupFactory = projectGroupFactory;
                this._projectGroupRepository = projectGroupRepository;
                this._publishExternalEventService = publishExternalEventService;
            }

            public async Task<Result<CreateProjectGroupOutputModel>> Handle(CreateProjectGroupCommand request, CancellationToken cancellationToken)
            {
                var projectGroup = this._projectGroupFactory
                                        .WithGroupName(request.GroupName)
                                        .WithOwner(request.OwnerId)
                                        .Build();

                await this._projectGroupRepository.Save(projectGroup, cancellationToken);

                var eventPayload = new ProjectGroupCreatedEvent(projectGroup.Id, request.OwnerId, projectGroup.GroupName);

                this._publishExternalEventService.Publish(eventPayload, "projectGroupExchange", "fanout", "");

                return Result<CreateProjectGroupOutputModel>.SuccessWith(new CreateProjectGroupOutputModel(projectGroup.Id));
            }
        }
    }
}
