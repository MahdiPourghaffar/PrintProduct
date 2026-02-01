using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.ToTable("productPrice");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Price)
            .HasColumnName("price");

        builder.Property(x => x.Circulation)
            .HasColumnName("Circulation");

        builder.Property(x => x.IsDoubleSided)
            .HasColumnName("is_double_sided");

        builder.Property(x => x.PageCount)
            .HasColumnName("pageCount")
            .IsRequired(false);

        builder.Property(x => x.CopyCount)
            .HasColumnName("copyCount")
            .IsRequired(false);

        builder.Property(x => x.ProductSizeId)
            .HasColumnName("productSize_id");

        builder.Property(x => x.ProductMaterialId)
            .HasColumnName("productMaterial_id");

        builder.Property(x => x.ProductMaterialAttributeId)
            .IsRequired(false);

        builder.Property(x => x.IsJeld)
            .HasColumnName("isJeld")
            .HasDefaultValue(0);;

        builder.Property(x => x.ProductPrintKindId)
            .HasColumnName("productPrintKind_id");
    }
}
