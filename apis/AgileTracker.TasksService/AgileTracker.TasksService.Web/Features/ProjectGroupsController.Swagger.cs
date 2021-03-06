﻿namespace AgileTracker.TasksService.Web.Features
{
    using System;
    using System.Collections.Generic;

    using AgileTracker.TasksService.Application.Features.Commands.AddMemberToProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.AddToProjectBacklog;
    using AgileTracker.TasksService.Application.Features.Commands.CreateProject;
    using AgileTracker.TasksService.Application.Features.Commands.CreateProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.CreateSprint;
    using AgileTracker.TasksService.Application.Features.Commands.FinishSprint;
    using AgileTracker.TasksService.Application.Features.Commands.InviteMemberToProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.RemoveFromProjectBacklog;
    using AgileTracker.TasksService.Application.Features.Commands.RemoveProject;
    using AgileTracker.TasksService.Application.Features.Commands.RemoveProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.RemoveSprint;
    using AgileTracker.TasksService.Application.Features.Commands.UpdateBacklogTask;
    using AgileTracker.TasksService.Application.Features.Commands.UpdateSprintTaskStatus;

    using Swashbuckle.AspNetCore.Filters;

    public static class ProjectGroupsSwaggerExamples
    {
        public class CreateProjectGroupExample : IExamplesProvider<CreateProjectGroupCommand>
        {
            public CreateProjectGroupCommand GetExamples()
                => new CreateProjectGroupCommand("groupName", "c210a166-28a6-4417-b71a-6ca777a0f493");
        }

        public class RemoveProjectGroupExample : IExamplesProvider<RemoveProjectGroupCommand>
        {
            public RemoveProjectGroupCommand GetExamples()
                => new RemoveProjectGroupCommand(1, "");
        }

        public class InviteMemberToProjectGroupExample : IExamplesProvider<InviteMemberToProjectGroupCommand>
        {
            public InviteMemberToProjectGroupCommand GetExamples()
                => new InviteMemberToProjectGroupCommand(1, "c210a166-28a6-4417-b71a-6ca777a0f493", "b6f4c506-6dcd-4373-907a-92d007840160");
        }

        public class AddMemberToProjectGroupExample : IExamplesProvider<AddMemberToProjectGroupCommand>
        {
            public AddMemberToProjectGroupCommand GetExamples()
                => new AddMemberToProjectGroupCommand(1, "b6f4c506-6dcd-4373-907a-92d007840160");
        }

        public class CreateProjectExample : IExamplesProvider<CreateProjectCommand>
        {
            public CreateProjectCommand GetExamples()
                => new CreateProjectCommand(1, "c210a166-28a6-4417-b71a-6ca777a0f493", "Project #1");
        }

        public class AddToProjectBacklogExample : IExamplesProvider<AddToProjectBacklogCommand>
        {
            public AddToProjectBacklogCommand GetExamples()
                => new AddToProjectBacklogCommand(1, 4, "", "Task 1", "Sample task", 2, "c210a166-28a6-4417-b71a-6ca777a0f493", DateTime.Now);
        }

        public class RemoveFromProjectBacklogExample : IExamplesProvider<RemoveFromProjectBacklogCommand>
        {
            public RemoveFromProjectBacklogCommand GetExamples()
                => new RemoveFromProjectBacklogCommand(1, 4, "c210a166-28a6-4417-b71a-6ca777a0f493", 2);
        }

        public class UpdateBacklogTaskExample : IExamplesProvider<UpdateBacklogTaskCommand>
        {
            public UpdateBacklogTaskCommand GetExamples()
                => new UpdateBacklogTaskCommand(1, 4, 5, "", "Task 1", "Sample task", 2, "c210a166-28a6-4417-b71a-6ca777a0f493");
        }

        public class RemoveProjectExample : IExamplesProvider<RemoveProjectCommand>
        {
            public RemoveProjectCommand GetExamples()
                => new RemoveProjectCommand(1, 4, "");
        }

        public class CreateSprintExample : IExamplesProvider<CreateSprintCommand>
        {
            public CreateSprintCommand GetExamples()
                => new CreateSprintCommand(1, 4, "c210a166-28a6-4417-b71a-6ca777a0f493", new List<int>(){ 1 }, DateTime.Now, 2);
        }

        public class UpdateSprintTaskStatusExample : IExamplesProvider<UpdateSprintTaskStatusCommand>
        {
            public UpdateSprintTaskStatusCommand GetExamples()
                => new UpdateSprintTaskStatusCommand(1, 4, 5, 1, "", "Pending");
        }

        public class FinishSprintExample : IExamplesProvider<FinishSprintCommand>
        {
            public FinishSprintCommand GetExamples()
                => new FinishSprintCommand(1, 4, 5, "");
        }

        public class RemoveSprintExample : IExamplesProvider<RemoveSprintCommand>
        {
            public RemoveSprintCommand GetExamples()
                => new RemoveSprintCommand(1, 4, 5, "");
        }
    }
}
