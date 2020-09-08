namespace AgileTracker.TasksService.Domain.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using AgileTracker.Common.Domain;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Domain.Exceptions;
    using AgileTracker.TasksService.Domain.Models.Entities;
    using AgileTracker.TasksService.Domain.Models.ValueObjects;

    public class ProjectGroup : Entity<int>, IAggregateRoot
    {
        private string _groupName = default!;
        private HashSet<ProjectGroupMember> _members;
        private HashSet<ProjectGroupInvitation> _invitations;

        internal ProjectGroup(string groupName, string ownerId)
        {
            this.GroupName = groupName;
            this._members = new HashSet<ProjectGroupMember>
            {
                new ProjectGroupMember(ownerId, true)
            };

            this._invitations = new HashSet<ProjectGroupInvitation>();
        }

        private ProjectGroup()
        {
            this._members = new HashSet<ProjectGroupMember>();
            this._invitations = new HashSet<ProjectGroupInvitation>();
        }

        public string GroupName
        {
            get => this._groupName;
            private set
            {
                Guard.AgainstEmptyString<InvalidProjectGroupException>(value, nameof(this.GroupName));
                this._groupName = value;
            }
        }

        public IReadOnlyCollection<ProjectGroupMember> Members
            => this._members.ToList().AsReadOnly();
        public IReadOnlyCollection<ProjectGroupInvitation> Invitations
            => this._invitations.ToList().AsReadOnly();

        public void AddGroupMember(string memberId)
        {
            var invitation = this._invitations.FirstOrDefault(i => i.InvitedMemberId == memberId);
            Guard.Against<InvalidProjectGroupException>(invitation, null!, nameof(invitation));

            this._invitations.Remove(invitation);
            this._members.Add(new ProjectGroupMember(memberId, false));
        }

        public void AddInvitation(string memberId)
            => this._invitations.Add(new ProjectGroupInvitation(memberId));
    }
}
