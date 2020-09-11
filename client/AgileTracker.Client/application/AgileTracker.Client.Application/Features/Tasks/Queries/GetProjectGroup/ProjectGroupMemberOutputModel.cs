﻿namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup
{
    public class ProjectGroupMemberOutputModel
    {
        public int Id { get; set; }

        public string MemberId { get; set; }

        public string UserName { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool IsOwner { get; set; }
    }
}