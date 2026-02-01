using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductAdtTypeConfiguration : IEntityTypeConfiguration<ProductAdtType>
{
    public void Configure(EntityTypeBuilder<ProductAdtType> builder)
    {
        builder.ToTable("productAdtType");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ProductAdtId).HasColumnName("productAdt_id");
        builder.Property(x => x.AdtTypeId).HasColumnName("adtType_id");

        builder.HasOne(x => x.ProductAdt)
            .WithMany(x => x.Types)
            .HasForeignKey(x => x.ProductAdtId);
    }
}
