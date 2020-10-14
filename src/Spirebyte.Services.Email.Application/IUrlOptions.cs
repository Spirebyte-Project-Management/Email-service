using System;
using System.Collections.Generic;
using System.Text;

namespace Spirebyte.Services.Email.Application
{
    public interface IUrlOptions
    {
        string ClientUrl { get; set; }
        string ResetPasswordPath { get; set; }
    }
}
