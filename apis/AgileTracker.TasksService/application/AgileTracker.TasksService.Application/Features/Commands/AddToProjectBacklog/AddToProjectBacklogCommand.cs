namespace AgileTracker.TasksService.Application.Features.Commands.AddToProjectBacklog
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Common.Application.Exceptions;
    using AgileTracker.TasksService.Application.Contracts;
    using AgileTracker.TasksService.Domain.Builders;

    using MediatR;

    public class AddToProjectBacklogCommand : AddToProjectBacklogInputModel, IRequest<Result>
    {
        public AddToProjectBacklogCommand(
            int projectGroupId,
            int projectId,
            string memberId,
            string title,
            string description,
            int pointsEstimate,
            DateTime startsOn)
            : base(projectGroupId, projectId, memberId, title, description, pointsEstimate, startsOn)
        {
        }

        public class AddToProjectBacklogCommandHandler : IRequestHandler<AddToProjectBacklogCommand, Result>
        {
            private readonly IProjectGroupRepository _projectGroupRepository;
            private readonly ITaskDescriptionBuilder _taskDescriptionBuilder;

            public AddToProjectBacklogCommandHandler(
                IProjectGroupRepository projectGroupRepository, 
                ITaskDescriptionBuilder taskDescriptionBuilder)
            {
                this._projectGroupRepository = projectGroupRepository;
                this._taskDescriptionBuilder = taskDescriptionBuilder;
            }

            public async Task<Result> Handle(AddToProjectBacklogCommand request, CancellationToken cancellationToken)
            {
                bool isMember = await this._projectGroupRepository.IsMember(request.ProjectGroupId, request.MemberId);

                if (!isMember)
                {
                    throw new ModelValidationException();
                }

                var projectGroup = await this._projectGroupRepository.GetById(request.ProjectGroupId);

                var project = projectGroup.Projects.FirstOrDefault(p => p.Id == request.ProjectId);

                var taskDescription = this._taskDescriptionBuilder
                                                .WithTitle(request.Title)
                                                .WithDescription(request.Description)
                                                .WithPointsEstimation(request.PointsEstimate)
                                                .WithAssignedToMemberId(request.MemberId)
                                                .Build();

                project.AddToBacklog(taskDescription, request.StartsOn);

                await this._projectGroupRepository.Save(projectGroup);

                return true;
            }
        }
    }
}
