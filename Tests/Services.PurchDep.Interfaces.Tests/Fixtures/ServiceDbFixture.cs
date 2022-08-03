using Microsoft.EntityFrameworkCore;
using PurchDep.Dal;
using Services.PurchDep.Interfaces.Tests.Data;

namespace Services.PurchDep.Interfaces.Tests.Fixtures
{
    public class ServiceDbFixture
    {
        public PurchDepContext Db { get; private set; }

        public ServiceDbFixture()
        {
            var builder = new DbContextOptionsBuilder<PurchDepContext>();
            builder.UseInMemoryDatabase("Services.PurchDep.Interfaces.Tests.InMemoryDb.Service");
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
            Db.Products.AddRange(ServicesTestData.AllProducts);
        }

        private void AddSuppliers()
        {
            Db.Suppliers.AddRange(ServicesTestData.AllSuppliers);
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
