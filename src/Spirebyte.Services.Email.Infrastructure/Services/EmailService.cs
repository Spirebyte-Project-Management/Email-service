using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Spirebyte.Services.Email.Application.Services.Interfaces;
using Spirebyte.Services.Email.Core.Objects;
using Spirebyte.Services.Email.Infrastructure.Configuration;
using Spirebyte.Services.Email.Infrastructure.RazorRenderer.Interfaces;

namespace Spirebyte.Services.Email.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;


        public EmailService(EmailOptions emailOptions, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _emailOptions = emailOptions;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        public async Task SendViewBasedEmail<T>(EmailAddress toAdress, string subject, string viewName, T messageBody)
        {
            await SendViewBasedEmail<T>(new List<EmailAddress> { toAdress }, subject, viewName, messageBody);
        }

        public async Task SendViewBasedEmail<T>(List<EmailAddress> toAdresses, string subject, string viewName, T messageBody)
        {
            string body = await _razorViewToStringRenderer.RenderViewToStringAsync(viewName, messageBody);

            var emailMessage = new EmailMessage
            {
                Content = body,
                Subject = subject,
                FromAddresses = new List<EmailAddress> { new EmailAddress (_emailOptions.SenderName, _emailOptions.SenderEmail) },
                ToAddresses = toAdresses
            };

            await Send(emailMessage);
        }

        public async Task Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.Bcc.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = emailMessage.Subject;
            //We will say we are sending HTML. But there are options for plaintext etc. 
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Content
            };

            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
            using (var emailClient = new SmtpClient())
            {
                if (!emailClient.IsConnected)
                {
                    if (!_emailOptions.IsSsl)
                    {
                        emailClient.ServerCertificateValidationCallback = (object sender,
                            X509Certificate certificate,
                            X509Chain chain,
                            SslPolicyErrors sslPolicyErrors) => true;
                    }
                    await emailClient.ConnectAsync(_emailOptions.SmtpServer, _emailOptions.SmtpPort, _emailOptions.IsSsl);
                    await emailClient.AuthenticateAsync(_emailOptions.SmtpUsername, _emailOptions.SmtpPassword);


                    //Remove any OAuth functionality as we won't be using it. 
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                }
                //The last parameter here is to use SSL (Which you should!)
                await emailClient.SendAsync(message);

                await emailClient.DisconnectAsync(true);
            }
        }
    }
}
