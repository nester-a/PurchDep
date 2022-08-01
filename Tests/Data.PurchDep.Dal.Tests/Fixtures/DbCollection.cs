using Xunit;

namespace Data.PurchDep.Dal.Tests.Fixtures
{
    [CollectionDefinition("Database collection")]
    public class DbCollection : ICollectionFixture<DbFixture> { }
}
