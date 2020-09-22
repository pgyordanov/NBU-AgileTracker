namespace AgileTracker.StatisticsService.Application.Features.Queries.GetEstimationStatistics
{
    using System.Collections.Generic;
    using System.Linq;

    public class GetEstimationStatisticsOutputModel
    {
        public GetEstimationStatisticsOutputModel(
            int totalEstimations,
            int accurateEstimations,
            int overEstimations3Days,
            int underEstimations3Days,
            int overEstimationsMoreThan3Days,
            int underEstimationsMoreThan3Days,
            IEnumerable<EstimationDataSetEntryOutputModel> daysEstimatedVsActualDataSet)
        {
            this.TotalEstimations = totalEstimations;
            this.DaysEstimatedVsActualDataSet = daysEstimatedVsActualDataSet;
            this.OverEstimations3Days = overEstimations3Days;
            this.UnderEstimations3Days = underEstimations3Days;
            this.OverEstimationsMoreThan3Days = overEstimationsMoreThan3Days;
            this.UnderEstimationsMoreThan3Days = underEstimationsMoreThan3Days;
            this.AccurateEstimations = accurateEstimations;
        }

        public int TotalEstimations { get; }

        public int AccurateEstimations { get; }

        public int OverEstimations3Days { get; }

        public int UnderEstimations3Days { get; }

        public int OverEstimationsMoreThan3Days { get; }

        public int UnderEstimationsMoreThan3Days { get; }

        public IEnumerable<EstimationDataSetEntryOutputModel> DaysEstimatedVsActualDataSet { get; }

        public IEnumerable<double> DaysEstimatedVsActualDataSetXKeys
            => this.DaysEstimatedVsActualDataSet.Select(d => d.XVariable).Distinct().OrderBy(k => k);

        public IEnumerable<double> DaysEstimatedVsActualDataSetYKeys
            => this.DaysEstimatedVsActualDataSet.Select(d => d.YVariable).Distinct().OrderBy(k => k);
    }
}
