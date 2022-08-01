using Data.PurchDep.Dal.Tests.Fixtures;
using System.Linq;
using Xunit;

namespace Data.PurchDep.Dal.Tests
{
    [Collection("Database collection")]
    public class PurchDepContextTests
    {
        DbFixture _fixture;
        public PurchDepContextTests(DbFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void PurchDepContextProductContainsData()
        {
            int productsCount = _fixture.Db.Products.Count();
            var products = _fixture.Db.Products.ToArray();

            Assert.True(productsCount > 0 && productsCount == 2);

            for (int i = 1; i < 3; i++)
            {
                Assert.Equal(i, products[i - 1].Id);
            }
        }

        [Fact]
        public void PurchDepContextSuppliersContainsData()
        {
            int suppliersCount = _fixture.Db.Suppliers.Count();
            var suppliers = _fixture.Db.Suppliers.ToArray();

            Assert.True(suppliersCount > 0 && suppliersCount == 2);

            for (int i = 1; i < 3; i++)
            {
                Assert.Equal(i, suppliers[i - 1].Id);
                Assert.True(suppliers.Any());
                if(i == 1) Assert.Single(suppliers[i - 1].Products);
                if(i == 2) Assert.Equal(2, suppliers[i - 1].Products.Count());
            }
        }
    }
}
