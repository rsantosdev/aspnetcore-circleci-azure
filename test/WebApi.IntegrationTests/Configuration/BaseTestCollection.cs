using Xunit;

namespace WebApi.IntegrationTests.Configuration
{
    [CollectionDefinition("Base collection")]
    public abstract class BaseTestCollection : ICollectionFixture<BaseTestFixture>
    {
    }
}