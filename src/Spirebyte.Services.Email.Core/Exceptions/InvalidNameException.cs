﻿using Spirebyte.Services.Email.Core.Exceptions.Base;

namespace Spirebyte.Services.Email.Core.Exceptions
{
    public class InvalidNameException : DomainException
    {
        public override string Code { get; } = "invalid_name";

        public InvalidNameException(string name) : base($"Invalid name: {name}.")
        {
        }
    }
}
