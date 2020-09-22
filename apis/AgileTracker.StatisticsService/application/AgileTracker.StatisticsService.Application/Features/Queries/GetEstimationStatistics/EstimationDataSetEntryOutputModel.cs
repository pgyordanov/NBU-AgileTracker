namespace AgileTracker.StatisticsService.Application.Features.Queries.GetEstimationStatistics
{
    public class EstimationDataSetEntryOutputModel
    {
        public EstimationDataSetEntryOutputModel(double xVariable, string xVariableLabel, double yVariable, string yVariableLabel)
        {
            this.XVariable = xVariable;
            this.YVariable = yVariable;
            this.YVariableLabel = yVariableLabel;
            this.XVariableLabel = xVariableLabel;
        }

        public double XVariable { get; }

        public string XVariableLabel { get; }

        public double YVariable { get; }

        public string YVariableLabel { get; }
    }
}
