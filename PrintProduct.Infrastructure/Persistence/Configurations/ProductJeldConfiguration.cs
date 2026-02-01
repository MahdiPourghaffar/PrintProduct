using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductJeldConfiguration : IEntityTypeConfiguration<ProductJeld>
{
    public void Configure(EntityTypeBuilder<ProductJeld> builder)
    {
        builder.ToTable("productJeld");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ProductId).HasColumnName("product_Id");
        builder.Property(x => x.PrintSide).HasColumnName("print_side");
        builder.Property(x => x.FileExtension)
            .HasColumnName("fileExtension")
            .HasColumnType("nvarchar(max)")
            .UseCollation("SQL_Latin1_General_CP1_CI_AI");

        builder.Property(x => x.IsCmyk).HasColumnName("IsCmyk");
        builder.Property(x => x.CutMargin)
            .HasColumnName("cutMargin")
            .IsRequired(false);

        builder.Property(x => x.PrintMargin)
            .HasColumnName("printMargin")
            .HasColumnType("float")
            .IsRequired(false);


        builder.Property(x => x.IsCheckFile).HasColumnName("isCheckFile");
    }
}
