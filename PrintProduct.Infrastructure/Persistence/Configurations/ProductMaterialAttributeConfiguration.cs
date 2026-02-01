using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductMaterialAttributeConfiguration 
    : IEntityTypeConfiguration<ProductMaterialAttribute>
{
    public void Configure(EntityTypeBuilder<ProductMaterialAttribute> builder)
    {
        builder.ToTable("productMaterialAttribute");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ProductMaterialId).HasColumnName("productMaterial_id");
        builder.Property(x => x.MaterialAttributeId).HasColumnName("MaterialAttribute_id");

        builder.HasOne(x => x.ProductMaterial)
            .WithMany(x => x.Attributes)
            .HasForeignKey(x => x.ProductMaterialId);
    }
}
