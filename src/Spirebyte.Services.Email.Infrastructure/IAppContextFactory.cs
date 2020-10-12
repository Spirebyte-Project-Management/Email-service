using Spirebyte.Services.Email.Application;

namespace Spirebyte.Services.Email.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}