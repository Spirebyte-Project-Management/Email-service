using System;
using System.Collections.Generic;
using System.Text;

namespace Spirebyte.Services.Email.Application.DTO
{
    public class UserInvitedToProjectDto
    {
        public UserInvitedToProjectDto(string projectTitle, string username, string url)
        {
            ProjectTitle = projectTitle;
            Username = username;
            Url = url;
        }

        public string ProjectTitle { get; }
        public string Username { get; }
        public string Url { get; }
    }
}
