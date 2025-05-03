using Emitix.StockService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emitix.StockService.Data.Mappings;

public class ProductStockMapping : IEntityTypeConfiguration<ProductStock>
{
    public void Configure(EntityTypeBuilder<ProductStock> builder)
    {
        builder.ToTable("Stocks");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ProductCode)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnType("INT");
        
        builder.Property(x=> x.UpdatedAt)
            .IsRequired()
            .HasColumnType("DATETIME2");

        builder.Property(x => x.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}