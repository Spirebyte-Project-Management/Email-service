using Spirebyte.Services.Email.Application;

namespace Spirebyte.Services.Email.Infrastructure.Configuration
{
    public class UrlOptions : IUrlOptions
    {
        public string ClientUrl { get; set; }
        public string ResetPasswordPath { get; set; }
        public string UserInvitedToProjectPath { get; set; }
    }
}
