﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Spirebyte.Services.Email.Infrastructure.Configuration
{
    public class EmailOptions
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public bool IsSsl { get; set; }
    }
}
