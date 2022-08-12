using Xunit;

namespace PurchDep.Interfaces.Tests.Services.Fixtures
{
    [CollectionDefinition("Service collection")]
    public class ServiceCollection : ICollectionFixture<ServiceFixture> { }
}
