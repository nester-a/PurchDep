using Xunit;

namespace Services.PurchDep.Interfaces.Tests.Fixtures
{
    [CollectionDefinition("Service Database collection")]
    public class ServiceDbCollection : ICollectionFixture<ServiceDbFixture> { }
}
