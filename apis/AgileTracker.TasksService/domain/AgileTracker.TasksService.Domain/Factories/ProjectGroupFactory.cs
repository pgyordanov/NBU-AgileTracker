namespace AgileTracker.TasksService.Domain.Factories
{
    using AgileTracker.TasksService.Domain.Models;

    public class ProjectGroupFactory : IProjectGroupFactory
    {
        private string _groupName = default!;
        private string _ownerId = default!;

        public IProjectGroupFactory WithGroupName(string groupName)
        {
            this._groupName = groupName;

            return this;
        }

        public IProjectGroupFactory WithOwner(string ownerId)
        {
            this._ownerId = ownerId;

            return this;
        }

        public ProjectGroup Build()
        {
            return new ProjectGroup(this._groupName, this._ownerId);
        }
    }
}
