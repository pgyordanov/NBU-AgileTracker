namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Index
{
    using System;

    using AgileTracker.Client.Application.Features.Tasks.Queries.GetTaskEstimations;
    using AgileTracker.Common.Application.Mapping;

    using AutoMapper;

    public class GetTaskEstimationsViewModel: IMapFrom<GetTaskEstimationsOutputModel>
    {
        public int TaskId { get; private set; }

        public int ProjectGroupId { get; private set; }

        public int ProjectId { get; private set; }

        public string EstimatedByMemberId { get; private set; } = default!;

        public DateTime StartedOn { get; private set; }

        public DateTime EstimatedToFinishOn { get; private set; }

        public DateTime ActuallyFinishedOn { get; private set; }

        public bool IsCompleted { get; private set; }

        public void Mapping(Profile mapping)
        {
            mapping.CreateMap<GetTaskEstimationsOutputModel, GetTaskEstimationsViewModel>()
                .ForMember(m => m.IsCompleted, memberOptions =>
                  {
                      memberOptions.MapFrom(m => m.ActuallyFinishedOn != default ? true : false);
                  });
        }
    }
}
