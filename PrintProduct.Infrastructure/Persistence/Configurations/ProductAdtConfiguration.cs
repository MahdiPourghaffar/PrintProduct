using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductAdtConfiguration : IEntityTypeConfiguration<ProductAdt>
{
    public void Configure(EntityTypeBuilder<ProductAdt> builder)
    {
        builder.ToTable("productAdt");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.AdtId).HasColumnName("adt_id");
        builder.Property(x => x.ProductId).HasColumnName("product_id");

        builder.Property(x => x.Required)
            .HasColumnName("required")
            .HasDefaultValue(0);

        builder.Property(x => x.Side).HasColumnName("side");
        builder.Property(x => x.Count).HasColumnName("count");
        builder.Property(x => x.IsJeld).HasColumnName("isJeld");

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Adts)
            .HasForeignKey(x => x.ProductId);
    }
}
