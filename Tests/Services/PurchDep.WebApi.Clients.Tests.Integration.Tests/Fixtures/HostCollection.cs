using Xunit;

namespace PurchDep.WebApi.Clients.Tests.Integration.Tests.Fixtures
{
    [CollectionDefinition("WebApi Client collection")]
    public class HostCollection : ICollectionFixture<HostFixture> { }
}
