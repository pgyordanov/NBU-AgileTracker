namespace AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstmationStatistics
{
    public class EstimationDataSetEntryOutputModel
    {
        public double XVariable { get; set; }

        public string XVariableLabel { get; set; } = default!;

        public double YVariable { get; set; }

        public string YVariableLabel { get; set; } = default!;
    }
}