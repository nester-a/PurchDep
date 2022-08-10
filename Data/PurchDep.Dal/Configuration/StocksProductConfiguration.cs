using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchDep.Dal.Entities;

namespace PurchDep.Dal.Configuration
{
    public class StocksProductConfiguration : IEntityTypeConfiguration<StocksProduct>
    {
        public void Configure(EntityTypeBuilder<StocksProduct> builder)
        {
            builder.HasKey(p => new { p.ProductId, p.SupplierId, p.StockId });
            builder.Property(p => p.Quantity).IsRequired();
        }
    }
}
