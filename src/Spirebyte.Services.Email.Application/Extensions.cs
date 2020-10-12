using Convey;
using Convey.CQRS.Events;

namespace Spirebyte.Services.Email.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
                .AddEventHandlers()
                .AddInMemoryEventDispatcher();
    }
}
