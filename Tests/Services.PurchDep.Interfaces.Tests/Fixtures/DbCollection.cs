using Xunit;

namespace Services.PurchDep.Interfaces.Tests.Fixtures
{
    [CollectionDefinition("Repo Database collection")]
    public class DbCollection : ICollectionFixture<DbFixture> { }
}
