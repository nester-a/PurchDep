using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchDep.Dal.Entities;

namespace PurchDep.Dal.Configuration
{
    public class SuppliersProductConfiguration : IEntityTypeConfiguration<SuppliersProduct>
    {
        public void Configure(EntityTypeBuilder<SuppliersProduct> builder)
        {
            builder.HasKey(p => new { p.ProductId, p.SupplierId });
            builder.Property(p => p.Price).IsRequired();
        }
    }
}
