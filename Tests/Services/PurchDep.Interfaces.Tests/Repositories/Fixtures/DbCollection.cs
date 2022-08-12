using Xunit;

namespace PurchDep.Interfaces.Tests.Repositories.Fixtures
{
    [CollectionDefinition("Repo Database collection")]
    public class DbCollection : ICollectionFixture<DbFixture> { }
}
