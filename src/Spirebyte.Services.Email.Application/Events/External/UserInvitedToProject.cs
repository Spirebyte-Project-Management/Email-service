﻿using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Spirebyte.Services.Email.Application.Events.External
{
    [Message("projects")]
    public class UserInvitedToProject : IEvent
    {
        public Guid ProjectId { get; }
        public string ProjectKey { get; }
        public string ProjectTitle { get; }
        public Guid UserId { get; }
        public string Username { get; }
        public string EmailAdress { get; }

        public UserInvitedToProject(Guid projectId, Guid userId, string projectTitle, string projectKey, string username, string emailAdress)
        {
            ProjectId = projectId;
            UserId = userId;
            ProjectTitle = projectTitle;
            ProjectKey = projectKey;
            Username = username;
            EmailAdress = emailAdress;
        }
    }
}