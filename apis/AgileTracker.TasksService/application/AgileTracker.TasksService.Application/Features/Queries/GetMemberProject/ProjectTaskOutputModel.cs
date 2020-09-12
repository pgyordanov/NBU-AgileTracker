namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProject
{
    using System;

    using AgileTracker.Common.Application.Mapping;
    using AgileTracker.TasksService.Domain.Models.Entities;

    using AutoMapper;

    public class ProjectTaskOutputModel : IMapFrom<Task>
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public string AssignedToMemberId { get; private set; } = default!;

        public int PointsEstimate { get; private set; }

        public ProjectTaskStatusOutputModel Status { get; private set; } = default!;

        public DateTime StartedOn { get; private set; }

        public DateTime FinishedOn { get; private set; }

        public bool IsFinished { get; private set; }

        public void Mapping(Profile mapping)
        {
            mapping.CreateMap<Task, ProjectTaskOutputModel>()
                .ForMember(m => m.Title, memberOptions =>
                {
                    memberOptions.MapFrom(t => t.Data.Title);
                })
                .ForMember(m => m.Description, memberOptions =>
                {
                    memberOptions.MapFrom(t => t.Data.Description);
                })
                .ForMember(m => m.PointsEstimate, memberOptions =>
                {
                    memberOptions.MapFrom(t => t.Data.PointsEstimate);
                })
                .ForMember(m => m.AssignedToMemberId, memberOptions =>
                {
                    memberOptions.MapFrom(t => t.Data.AssignedToMemberId);
                });
        }
    }
}