using Microsoft.EntityFrameworkCore;
using PurchDep.Dal;
using System;

namespace PurchDep.Interfaces.Tests.Repositories.Fixtures
{
    public class DbFixture : IDisposable
    {
        public PurchDepContext Db { get; private set; }

        public DbFixture()
        {
            var builder = new DbContextOptionsBuilder<PurchDepContext>();
            builder.UseInMemoryDatabase("Services.PurchDep.Interfaces.Tests.InMemoryDb.Repo");
            builder.EnableSensitiveDataLogging();

            var options = builder.Options;

            Db = new PurchDepContext(options);

            Db.Database.EnsureDeleted();
            Db.Database.EnsureCreated();

            AddTestData();

            Db.SaveChanges();
        }

        private void AddTestData()
        {
            Db?.Products.Add(TestData.TestData.ProductDal_1);
            Db?.Suppliers.Add(TestData.TestData.SupplierDal_1);
            Db?.Stocks.Add(TestData.TestData.StockDal_1);
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
