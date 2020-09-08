namespace AgileTracker.TasksService.Domain.Factories
{
    using AgileTracker.Common.Domain.Factories;
    using AgileTracker.TasksService.Domain.Models;

    public interface IProjectGroupFactory : IFactory<ProjectGroup>
    {
        IProjectGroupFactory WithGroupName(string groupName);

        IProjectGroupFactory WithOwner(string ownerId);
    }
}
