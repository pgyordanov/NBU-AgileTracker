namespace AgileTracker.Client.Startup.Models.Tasks.Estimations
{
    using AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstmationStatistics;
    using AgileTracker.Common.Application.Mapping;

    using AutoMapper;

    public class EstimationDataSetEntryViewModel : IMapFrom<EstimationDataSetEntryOutputModel>
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public void Mapping(Profile mapping)
        {
            mapping.CreateMap<EstimationDataSetEntryOutputModel, EstimationDataSetEntryViewModel>()
                .ForMember(m => m.X, memberOptions => memberOptions.MapFrom(s => s.XVariable))
                .ForMember(m => m.Y, memberOptions => memberOptions.MapFrom(s => s.YVariable));
        }
    }
}