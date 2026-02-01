using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductDeliverConfiguration : IEntityTypeConfiguration<ProductDeliver>
{
    public void Configure(EntityTypeBuilder<ProductDeliver> builder)
    {
        builder.ToTable("productDeliver");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ProductId).HasColumnName("product_id");
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.IsIncreased).HasColumnName("isIncreased");
        builder.Property(x => x.StartCirculation).HasColumnName("startCirculation");
        builder.Property(x => x.EndCirculation).HasColumnName("endCirculation");
        builder.Property(x => x.PrintSide).HasColumnName("print_side");
        builder.Property(x => x.Price)
            .HasColumnName("price")
            .HasColumnType("float");

        builder.Property(x => x.CalcType).HasColumnName("calcType");

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Delivers)
            .HasForeignKey(x => x.ProductId);
    }
}
