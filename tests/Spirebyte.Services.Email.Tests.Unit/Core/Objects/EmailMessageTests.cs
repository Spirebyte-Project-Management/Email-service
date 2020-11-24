using System;
using FluentAssertions;
using Spirebyte.Services.Email.Core.Exceptions;
using Spirebyte.Services.Email.Core.Objects;
using Spirebyte.Services.Email.Core.Objects.Base;
using Xunit;

namespace Spirebyte.Services.Email.Tests.Unit.Core.Objects
{
    public class EmailMessageTests
    {
        [Fact]
        public void given_valid_input_email_message_should_be_created()
        {
            var emailAddress = new EmailMessage();

            emailAddress.Should().NotBeNull();
        }
    }
}
