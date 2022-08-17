using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchDep.Dal.Entities;

namespace PurchDep.Dal.Configuration
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.Id).IsUnique();
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).IsRequired();
            builder.HasMany(s => s.StocksProducts).WithOne(p => p.Stock).HasForeignKey(p => p.StockId);
        }
    }
}
