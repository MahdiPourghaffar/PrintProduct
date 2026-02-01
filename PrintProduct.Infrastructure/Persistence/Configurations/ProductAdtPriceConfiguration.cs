using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductAdtPriceConfiguration : IEntityTypeConfiguration<ProductAdtPrice>
{
    public void Configure(EntityTypeBuilder<ProductAdtPrice> builder)
    {
        builder.ToTable("productAdtPrice");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ProductAdtId).HasColumnName("productAdt_id");
        builder.Property(x => x.ProductPriceId).HasColumnName("productPrice_id");
        builder.Property(x => x.ProductAdtTypeId).HasColumnName("productAdtType_id");
        builder.Property(x => x.Price)
            .HasColumnName("price")
            .HasColumnType("float");

        builder.HasOne(x => x.ProductAdt)
            .WithMany(x => x.Prices)
            .HasForeignKey(x => x.ProductAdtId);
    }
}
