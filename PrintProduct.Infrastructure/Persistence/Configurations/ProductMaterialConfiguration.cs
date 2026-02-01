using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductMaterialConfiguration : IEntityTypeConfiguration<ProductMaterial>
{
    public void Configure(EntityTypeBuilder<ProductMaterial> builder)
    {
        builder.ToTable("productMaterial");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id");

        builder.Property(x => x.MaterialId)
            .HasColumnName("Material_id");

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(50)
            .HasColumnType("nvarchar(max)")
            .UseCollation("SQL_Latin1_General_CP1_CI_AI");

        builder.Property(x => x.IsJeld)
            .HasColumnName("isJeld")
            .HasDefaultValue(0);

        builder.Property(x => x.Required)
            .HasColumnName("required")
            .HasDefaultValue(0);

        builder.Property(x => x.IsCustomCirculation)
            .HasColumnName("is_custom_circulation")
            .HasDefaultValue(0);

        builder.Property(x => x.IsCombinedMaterial)
            .HasColumnName("is_combined_material")
            .HasDefaultValue(0);

        builder.Property(x => x.Weight)
            .HasColumnName("Weight")
            .IsRequired(false);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Materials)
            .HasForeignKey(x => x.ProductId);
    }
}
