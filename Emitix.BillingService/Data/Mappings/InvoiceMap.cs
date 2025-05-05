using Emitix.BillingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emitix.BillingService.Data.Mappings;

public class InvoiceMap : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoice");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();
        
        builder.Property(x=> x.Number)
            .IsRequired()
            .HasColumnType("INT");
        
        builder.Property(x => x.Series)
            .IsRequired()
            .HasMaxLength(10)            
            .HasColumnType("VARCHAR");
        
        builder.Property(x=> x.InvoiceStatus)
            .IsRequired()
            .HasConversion<int>()
            .HasColumnType("TINYINT");
        
        builder.Property(x=> x.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIME2");
        
        builder.HasMany(x=> x.Products)
            .WithOne(x=> x.Invoice)
            .HasForeignKey(x=> x.InvoiceId);

        builder.HasIndex(x => new { x.Number, x.Series }).IsUnique();
    }
}