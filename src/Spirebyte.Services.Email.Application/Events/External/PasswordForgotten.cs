using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;

namespace Spirebyte.Services.Email.Application.Events.External
{
    [Message("identity")]

    public class PasswordForgotten : IEvent
    {
        public Guid UserId { get; }
        public string Fullname { get; }
        public string Email { get; }
        public string Token { get; }

        public PasswordForgotten(Guid userId, string fullname, string email, string token)
        {
            UserId = userId;
            Fullname = fullname;
            Email = email;
            Token = token;
        }
    }
}
