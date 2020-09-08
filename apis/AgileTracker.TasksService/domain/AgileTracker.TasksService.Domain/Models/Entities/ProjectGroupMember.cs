namespace AgileTracker.TasksService.Domain.Models.Entities
{
    using AgileTracker.Common.Domain;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Domain.Exceptions;

    public class ProjectGroupMember: Entity<int>
    {
        private string _memberId = default!;

        internal ProjectGroupMember(string memberId, bool isOwner)
        {
            this.MemberId = memberId;
            this.IsOwner = isOwner;
        }

        private ProjectGroupMember()
        {
        }

        public string MemberId
        {
            get => this._memberId;
            private set
            {
                Guard.AgainstEmptyString<InvalidProjectGroupException>(value, nameof(this.MemberId));
                this._memberId = value;
            }
        }

        public bool IsOwner { get; private set; }
    }
}
