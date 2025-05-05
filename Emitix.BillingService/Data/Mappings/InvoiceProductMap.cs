using Emitix.BillingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emitix.BillingService.Data.Mappings;

public class InvoiceProductMap : IEntityTypeConfiguration<InvoiceProduct>
{
    public void Configure(EntityTypeBuilder<InvoiceProduct> builder)
    {
        builder.ToTable("InvoiceProduct");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("VARCHAR");

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasPrecision(14, 4);
        
        builder.Property(x => x.UnitPrice)
            .IsRequired()
            .HasPrecision(12, 2);

        builder.Ignore(x => x.Subtotal);
    }
}