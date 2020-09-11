namespace AgileTracker.TasksService.Web.Features
{
    using AgileTracker.TasksService.Application.Features.Commands.AddMemberToProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.CreateProject;
    using AgileTracker.TasksService.Application.Features.Commands.CreateProjectGroup;
    using AgileTracker.TasksService.Application.Features.Commands.InviteMemberToProjectGroup;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups;

    using Swashbuckle.AspNetCore.Filters;

    public static class ProjectGroupsSwaggerExamples 
    {
        public class CreateProjectGroupExample : IExamplesProvider<CreateProjectGroupCommand>
        {
            public CreateProjectGroupCommand GetExamples()
                => new CreateProjectGroupCommand("groupName", "c210a166-28a6-4417-b71a-6ca777a0f493");
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
    }
}
