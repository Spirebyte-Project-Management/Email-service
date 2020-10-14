﻿using System;
using Spirebyte.Services.Email.Application.Exceptions.Base;

namespace Spirebyte.Services.Email.Application.Exceptions
{
    public class UserAlreadyCreatedException : AppException
    {
        public override string Code { get; } = "user_already_created";
        public Guid UserId { get; }

        public UserAlreadyCreatedException(Guid userId)
            : base($"User with id: {userId} was already created.")
        {
            UserId = userId;
        }
    }
}