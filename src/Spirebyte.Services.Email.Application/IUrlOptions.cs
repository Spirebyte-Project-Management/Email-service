namespace Spirebyte.Services.Email.Application
{
    public interface IUrlOptions
    {
        string ClientUrl { get; set; }
        string ResetPasswordPath { get; set; }

        string UserInvitedToProjectPath { get; set; }
    }
}
