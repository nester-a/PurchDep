using Xunit;

namespace PurchDep.WebApi.Tests.Fixtures
{
    [CollectionDefinition("WebApi Database collection")]
    public class ServiceDbCollection : ICollectionFixture<DbFixture> { }
}
