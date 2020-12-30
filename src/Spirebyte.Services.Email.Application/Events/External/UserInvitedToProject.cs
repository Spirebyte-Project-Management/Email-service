using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;

namespace Spirebyte.Services.Email.Application.Events.External
{
    [Message("projects")]
    public class UserInvitedToProject : IEvent
    {
        public string ProjectId { get; }
        public string ProjectTitle { get; }
        public Guid UserId { get; }
        public string Username { get; }
        public string EmailAddress { get; }

        public UserInvitedToProject(string projectId, Guid userId, string projectTitle, string username, string emailAddress)
        {
            ProjectId = projectId;
            UserId = userId;
            ProjectTitle = projectTitle;
            Username = username;
            EmailAddress = emailAddress;
        }
    }
}
