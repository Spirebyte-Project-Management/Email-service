using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using Spirebyte.Services.Email.Application.DTO;
using Spirebyte.Services.Email.Application.Services.Interfaces;
using Spirebyte.Services.Email.Core.Objects;
using System.Threading.Tasks;

namespace Spirebyte.Services.Email.Application.Events.External.Handlers
{
    public class UserInvitedToProjectHandler : IEventHandler<UserInvitedToProject>
    {
        public const string PasswordForgottenTemplate = "/Views/Templates/UserInvitedToProjectEmail.cshtml";
        public const string Title = "You have been invited to a project";

        private readonly IEmailService _emailService;
        private readonly IUrlOptions _urlOptions;
        private readonly ILogger<UserInvitedToProjectHandler> _logger;

        public UserInvitedToProjectHandler(IEmailService emailService, IUrlOptions urlOptions, ILogger<UserInvitedToProjectHandler> logger)
        {
            _emailService = emailService;
            _urlOptions = urlOptions;
            _logger = logger;
        }

        public async Task HandleAsync(UserInvitedToProject @event)
        {
            var url = _urlOptions.ClientUrl + string.Format(_urlOptions.UserInvitedToProjectPath, @event.ProjectKey, @event.UserId);
            var emailAddress = new EmailAddress(@event.Username, @event.EmailAdress);

            await _emailService.SendViewBasedEmail<UserInvitedToProjectDto>(emailAddress, Title, PasswordForgottenTemplate, new UserInvitedToProjectDto(@event.ProjectTitle, @event.Username, url));
            _logger.LogInformation($"Sent project invitation to user with id: {@event.UserId}");
        }
    }
}
