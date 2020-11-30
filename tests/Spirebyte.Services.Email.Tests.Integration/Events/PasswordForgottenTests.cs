using Convey.CQRS.Events;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Spirebyte.Services.Email.API;
using Spirebyte.Services.Email.Application.Events.External;
using Spirebyte.Services.Email.Tests.Shared.Factories;
using Spirebyte.Services.Email.Tests.Shared.Fixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Spirebyte.Services.Email.Tests.Integration.Events
{
    [Collection("Spirebyte collection")]
    public class PasswordForgottenTests : IDisposable
    {
        public PasswordForgottenTests(SpirebyteApplicationFactory<Program> factory)
        {
            _rabbitMqFixture = new RabbitMqFixture();
            factory.Server.AllowSynchronousIO = true;
            _eventHandler = factory.Services.GetRequiredService<IEventHandler<PasswordForgotten>>();
        }

        public void Dispose()
        {
        }

        private const string Exchange = "email";
        private readonly RabbitMqFixture _rabbitMqFixture;
        private readonly IEventHandler<PasswordForgotten> _eventHandler;


        [Fact]
        public async Task password_forgotten_event_should_send_email_with_token()
        {
            var userId = Guid.NewGuid();
            var fullname = "name lastname";
            var email = "email@test.com";
            var token = "passwordForgottenToken";


            var externalEvent = new PasswordForgotten(userId, fullname, email, token);

            // Check if exception is thrown

            _eventHandler
                .Awaiting(c => c.HandleAsync(externalEvent))
                .Should().NotThrow();
        }
    }
}
