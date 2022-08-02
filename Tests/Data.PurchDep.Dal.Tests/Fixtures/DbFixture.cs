using Data.PurchDep.Dal.Tests.Data;
using Microsoft.EntityFrameworkCore;
using PurchDep.Dal;
using System;

namespace Data.PurchDep.Dal.Tests.Fixtures
{
    public class DbFixture : IDisposable
    {
        public PurchDepContext Db { get; private set; }

        public DbFixture()
        {
            var builder = new DbContextOptionsBuilder<PurchDepContext>();
            builder.UseInMemoryDatabase("Data.PurchDep.Dal.Tests.InMemoryDb");
            builder.EnableSensitiveDataLogging();

            var options = builder.Options;

            Db = new PurchDepContext(options);

            Db.Database.EnsureDeleted();
            Db.Database.EnsureCreated();

            AddProducts();
            AddSuppliers();


            Db.SaveChanges();
        }

        private void AddProducts()
        {
            Db.Products.AddRange(TestData.AllProducts);
        }

        private void AddSuppliers()
        {
            Db.Suppliers.AddRange(TestData.AllSuppliers);
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
