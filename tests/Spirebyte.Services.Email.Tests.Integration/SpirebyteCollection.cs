using Spirebyte.Services.Email.API;
using Spirebyte.Services.Email.Tests.Shared.Factories;
using Xunit;

namespace Spirebyte.Services.Email.Tests.Integration
{
    [CollectionDefinition("Spirebyte collection", DisableParallelization = true)]
    public class SpirebyteCollection : ICollectionFixture<SpirebyteApplicationFactory<Program>>
    {
    }
}