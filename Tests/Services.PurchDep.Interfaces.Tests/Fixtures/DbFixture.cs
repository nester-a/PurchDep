using Microsoft.EntityFrameworkCore;
using PurchDep.Dal;
using Services.PurchDep.Interfaces.Tests.Data;
using System;

namespace Services.PurchDep.Interfaces.Tests.Fixtures
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

            AddProducts();
            AddSuppliers();
            Db.SaveChanges();
        }

        private void AddProducts()
        {
            Db.Products.AddRange(RepoTestData.AllProducts);
        }

        private void AddSuppliers()
        {
            Db.Suppliers.AddRange(RepoTestData.AllSuppliers);
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
