using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using Spirebyte.Services.Email.Application.Services.Interfaces;
using Spirebyte.Services.Email.Core.Objects;
using System.Threading.Tasks;

namespace Spirebyte.Services.Email.Application.Events.External.Handlers
{
    public class PasswordForgottenHandler : IEventHandler<PasswordForgotten>
    {
        public const string PasswordForgottenTemplate = "/Views/Templates/ForgotPasswordEmail.cshtml";
        public const string Title = "Forgot ur password?";

        private readonly IEmailService _emailService;
        private readonly IUrlOptions _urlOptions;
        private readonly ILogger<PasswordForgottenHandler> _logger;

        public PasswordForgottenHandler(IEmailService emailService, IUrlOptions urlOptions, ILogger<PasswordForgottenHandler> logger)
        {
            _emailService = emailService;
            _urlOptions = urlOptions;
            _logger = logger;
        }

        public async Task HandleAsync(PasswordForgotten @event)
        {
            var url = _urlOptions.ClientUrl + string.Format(_urlOptions.ResetPasswordPath, @event.UserId, @event.Token);
            var emailAddress = new EmailAddress(@event.Fullname, @event.Email);

            await _emailService.SendViewBasedEmail<string>(emailAddress, Title, PasswordForgottenTemplate, url);
            _logger.LogInformation($"Sent pasword reset email for user with id: {@event.UserId}");
        }
    }
}
