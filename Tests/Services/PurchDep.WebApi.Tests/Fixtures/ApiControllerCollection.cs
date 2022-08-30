using Xunit;

namespace PurchDep.WebApi.Tests.Fixtures
{
    [CollectionDefinition("ApiController collection")]
    public class ApiControllerCollection : ICollectionFixture<ApiControllerFixture> { }
}
