namespace AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstmationStatistics
{
    using System.Collections.Generic;

    public class GetTaskEstimationStatisticsOutputModel
    {
        public int TotalEstimations { get; set; }

        public int AccurateEstimations { get; set; }

        public int OverEstimations3Days { get; set; }

        public int UnderEstimations3Days { get; set; }

        public int OverEstimationsMoreThan3Days { get; set; }

        public int UnderEstimationsMoreThan3Days { get; set; }

        public IEnumerable<EstimationDataSetEntryOutputModel> DaysEstimatedVsActualDataSet { get; set; } = default!;

        public IEnumerable<double> DaysEstimatedVsActualDataSetXKeys { get; set; } = default!;

        public IEnumerable<double> DaysEstimatedVsActualDataSetYKeys { get; set; } = default!;
    }
}
