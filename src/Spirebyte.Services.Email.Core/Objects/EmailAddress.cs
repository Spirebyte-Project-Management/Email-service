using Spirebyte.Services.Email.Core.Exceptions;

namespace Spirebyte.Services.Email.Core.Objects
{
    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public EmailAddress(string name, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidNameException(name);
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new InvalidEmailAddressException(address);
            }

            Name = name;
            Address = address;
        }
    }
}
