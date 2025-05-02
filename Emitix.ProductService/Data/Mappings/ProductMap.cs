using Emitix.ProductService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emitix.ProductService.Data.Mappings;

public record ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        
        builder.HasKey(p => p.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        
        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("MONEY");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnType("TINYINT")
            .HasConversion<int>();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIME2");
        
        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasColumnType("DATETIME2");
        
        builder.HasIndex(p => p.Code).IsUnique();
        builder.HasIndex(p => p.Id).IsUnique();
    }
}