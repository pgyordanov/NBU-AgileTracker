namespace AgileTracker.TasksService.Domain.Builders
{
    using AgileTracker.Common.Domain.Builders;
    using AgileTracker.TasksService.Domain.Models.ValueObjects;

    public interface ITaskDescriptionBuilder: IBuilder<TaskData>
    {
        ITaskDescriptionBuilder WithTitle(string title);

        ITaskDescriptionBuilder WithDescription(string description);

        ITaskDescriptionBuilder WithPointsEstimation(int pointsEstimation);

        ITaskDescriptionBuilder WithAssignedToMemberId(string assignedToMemberId);

    }
}
