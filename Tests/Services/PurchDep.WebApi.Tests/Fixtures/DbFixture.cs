﻿using Microsoft.EntityFrameworkCore;
using PurchDep.Dal;

namespace PurchDep.WebApi.Tests.Fixtures
{
    public class DbFixture
    {
        public PurchDepContext Db { get; private set; }

        public DbFixture()
        {
            var builder = new DbContextOptionsBuilder<PurchDepContext>();
            builder.UseInMemoryDatabase("Services.PurchDep.WebApi.Tests.InMemoryDb.Controller");
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
            Db.Products.AddRange(Data.TestData.AllProducts);
        }

        private void AddSuppliers()
        {
            Db.Suppliers.AddRange(Data.TestData.AllSuppliers);
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
