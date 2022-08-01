using Microsoft.EntityFrameworkCore;
using PurchDep.Dal.Configuration;
using PurchDep.Dal.Entities;

namespace PurchDep.Dal
{
    public class PurchDepContext : DbContext
    {
        public PurchDepContext(DbContextOptions<PurchDepContext> options) : base(options)
        {

        }
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
