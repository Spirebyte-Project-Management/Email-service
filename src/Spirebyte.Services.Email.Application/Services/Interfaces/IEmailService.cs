using Spirebyte.Services.Email.Core.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spirebyte.Services.Email.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);
        Task SendViewBasedEmail<T>(EmailAddress toAdress, string subject, string viewName, T messageBody);
        Task SendViewBasedEmail<T>(List<EmailAddress> toAdresses, string subject, string viewName, T messageBody);
    }
}
