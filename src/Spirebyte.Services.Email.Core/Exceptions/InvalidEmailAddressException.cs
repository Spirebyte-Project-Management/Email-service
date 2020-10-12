using Spirebyte.Services.Email.Core.Exceptions.Base;

namespace Spirebyte.Services.Email.Core.Exceptions
{
    public class InvalidEmailAddressException : DomainException
    {
        public override string Code { get; } = "invalid_emailaddress";

        public InvalidEmailAddressException(string address) : base($"Invalid email address: {address}.")
        {
        }
    }
}
