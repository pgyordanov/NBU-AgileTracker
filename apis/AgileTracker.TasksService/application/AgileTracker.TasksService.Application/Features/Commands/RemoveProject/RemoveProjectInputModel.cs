﻿namespace AgileTracker.TasksService.Application.Features.Commands.RemoveProject
{
    public class RemoveProjectInputModel
    {
        public RemoveProjectInputModel(
                  int projectGroupId,
                  int projectId,
                  string memberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.MemberId = memberId;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public string MemberId { get; }
    }
}
