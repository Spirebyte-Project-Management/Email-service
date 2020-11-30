using FluentAssertions;
using Spirebyte.Services.Email.Core.Exceptions;
using Spirebyte.Services.Email.Core.Objects;
using System;
using Xunit;

namespace Spirebyte.Services.Email.Tests.Unit.Core.Objects
{
    public class EmailAddressTests
    {
        [Fact]
        public void given_valid_input_email_address_should_be_created()
        {
            var name = "username";
            var address = "email@test.com";

            var emailAddress = new EmailAddress(name, address);

            emailAddress.Should().NotBeNull();
            emailAddress.Name.Should().Be(name);
            emailAddress.Address.Should().Be(address);
        }

        [Fact]
        public void given_empty_name_email_address_should_throw_an_exeption()
        {
            var name = string.Empty;
            var address = "email@test.com";

            Action act = () => new EmailAddress(name, address);
            act.Should().Throw<InvalidNameException>();
        }

        [Fact]
        public void given_empty_address_email_address_should_throw_an_exeption()
        {
            var name = "username";
            var address = string.Empty;

            Action act = () => new EmailAddress(name, address);
            act.Should().Throw<InvalidEmailAddressException>();
        }
    }
}
