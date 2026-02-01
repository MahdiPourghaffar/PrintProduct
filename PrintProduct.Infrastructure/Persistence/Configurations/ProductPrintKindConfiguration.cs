using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductPrintKindConfiguration : IEntityTypeConfiguration<ProductPrintKind>
{
    public void Configure(EntityTypeBuilder<ProductPrintKind> builder)
    {
        builder.ToTable("productPrintKind");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ProductId).HasColumnName("product_id");
        builder.Property(x => x.PrintKindId).HasColumnName("printKind_id");
        builder.Property(x => x.IsJeld).HasColumnName("isJeld");

        builder.HasOne(x => x.Product)
            .WithMany(x => x.PrintKinds)
            .HasForeignKey(x => x.ProductId);
    }
}
