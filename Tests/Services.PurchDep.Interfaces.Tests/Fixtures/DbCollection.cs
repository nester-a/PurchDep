using Xunit;

namespace Services.PurchDep.Interfaces.Tests.Fixtures
{
    [CollectionDefinition("Database collection")]
    public class DbCollection : ICollectionFixture<DbFixture> { }
}
