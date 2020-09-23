namespace AgileTracker.Client.Startup.Models.Tasks.Estimations
{
    using System.Collections.Generic;
    using System.Linq;

    using AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstmationStatistics;
    using AgileTracker.Common.Application.Mapping;

    using AutoMapper;

    public class GetStatisticsViewModel : IMapFrom<GetTaskEstimationStatisticsOutputModel>
    {
        public int TotalEstimations { get; private set; }

        public int AccurateEstimations { get; private set; }

        public int OverEstimations3Days { get; private set; }

        public int UnderEstimations3Days { get; private set; }

        public int OverEstimationsMoreThan3Days { get; private set; }

        public int UnderEstimationsMoreThan3Days { get; private set; }

        public IEnumerable<EstimationDataSetEntryViewModel> DaysEstimatedVsActualDataSet { get; private set; } = default!;

        public string DaysEstimatedVsActualDataSetXLabel { get; private set; } = default!;

        public string DaysEstimatedVsActualDataSetYLabel { get; private set; } = default!;

        public IEnumerable<double> DaysEstimatedVsActualDataSetXKeys { get; private set; } = default!;

        public IEnumerable<double> DaysEstimatedVsActualDataSetYKeys { get; private set; } = default!;

        public void Mapping(Profile mapping)
        {
            mapping.CreateMap<GetTaskEstimationStatisticsOutputModel, GetStatisticsViewModel>()
                .ForMember(m => m.DaysEstimatedVsActualDataSetXLabel, memberOptions =>
                {
                    memberOptions.Condition(s => s.DaysEstimatedVsActualDataSet.Any());
                    memberOptions.MapFrom(s => s.DaysEstimatedVsActualDataSet.FirstOrDefault().XVariableLabel);
                })
                .ForMember(m => m.DaysEstimatedVsActualDataSetYLabel, memberOptions =>
                {
                    memberOptions.Condition(s => s.DaysEstimatedVsActualDataSet.Any());
                    memberOptions.MapFrom(s => s.DaysEstimatedVsActualDataSet.FirstOrDefault().YVariableLabel);
                });
        }
    }
}
