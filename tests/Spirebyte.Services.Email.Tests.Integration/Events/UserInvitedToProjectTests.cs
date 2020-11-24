using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Spirebyte.Services.Email.API;
using Spirebyte.Services.Email.Application.Events.External;
using Spirebyte.Services.Email.Tests.Shared.Factories;
using Spirebyte.Services.Email.Tests.Shared.Fixtures;
using Xunit;

namespace Spirebyte.Services.Email.Tests.Integration.Events
{
    [Collection("Spirebyte collection")]
    public class UserInvitedToProjectTests : IDisposable
    {
        public UserInvitedToProjectTests(SpirebyteApplicationFactory<Program> factory)
        {
            _rabbitMqFixture = new RabbitMqFixture();
            factory.Server.AllowSynchronousIO = true;
            _eventHandler = factory.Services.GetRequiredService<IEventHandler<UserInvitedToProject>>();
        }

        public void Dispose()
        {
        }

        private const string Exchange = "email";
        private readonly RabbitMqFixture _rabbitMqFixture;
        private readonly IEventHandler<UserInvitedToProject> _eventHandler;


        [Fact]
        public async Task user_invited_to_project_event_should_send_email_with_link()
        {
            var projectId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var username = "username";
            var email = "email@test.com";
            var projectname = "projectTitle";
            var projectKey = "key-1";


            var externalEvent = new UserInvitedToProject(userId, userId, projectname, projectKey, username, email);

            // Check if exception is thrown

            _eventHandler
                .Awaiting(c => c.HandleAsync(externalEvent))
                .Should().NotThrow();
        }
    }
}
