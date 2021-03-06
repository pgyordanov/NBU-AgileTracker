﻿namespace AgileTracker.TasksService.Application.Features.Commands.RemoveProjectGroup
{
    public class RemoveProjectGroupInputModel
    {
        public RemoveProjectGroupInputModel(
                    int projectGroupId,
                    string memberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.MemberId = memberId;
        }

        public int ProjectGroupId { get; }

        public string MemberId { get; }
    }
}
