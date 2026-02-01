using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
{
    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.ToTable("productSize");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ProductId).HasColumnName("product_Id");
        builder.Property(x => x.Length)
            .HasColumnName("length")
            .HasColumnType("float");

        builder.Property(x => x.Width)
            .HasColumnName("width")
            .HasColumnType("float");

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(50)
            .HasColumnType("nvarchar(max)")
            .UseCollation("SQL_Latin1_General_CP1_CI_AI");

        builder.Property(x => x.SheetCount)
            .HasColumnName("sheetCount")
            .IsRequired(false);

        builder.Property(x => x.SheetDimensionId)
            .HasColumnName("sheetDimension_id")
            .IsRequired(false);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Sizes)
            .HasForeignKey(x => x.ProductId);
    }
}
