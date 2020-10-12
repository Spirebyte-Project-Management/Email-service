using System;
using Convey.MessageBrokers.RabbitMQ;
using Spirebyte.Services.Email.Application.Exceptions;

namespace Spirebyte.Services.Email.Infrastructure.Exceptions
{
    internal sealed class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch

            {
                _ => null
            };
    }
}
