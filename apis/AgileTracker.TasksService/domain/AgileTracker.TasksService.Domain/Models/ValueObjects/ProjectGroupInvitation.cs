namespace AgileTracker.TasksService.Domain.Models.ValueObjects
{
    using AgileTracker.Common.Domain;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Domain.Exceptions;

    public class ProjectGroupInvitation : ValueObject
    {
        private string _invitedMemberId = default!;

        internal ProjectGroupInvitation(string invitedMemberId)
        {
            this.InvitedMemberId = invitedMemberId;
        }

        private ProjectGroupInvitation()
        {
        }

        public string InvitedMemberId
        {
            get => this._invitedMemberId;
            private set
            {
                Guard.AgainstEmptyString<InvalidProjectGroupException>(value, nameof(this.InvitedMemberId));
                this._invitedMemberId = value;
            }
        }
    }
}
