using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductDeliverSizeConfiguration : IEntityTypeConfiguration<ProductDeliverSize>
{
    public void Configure(EntityTypeBuilder<ProductDeliverSize> builder)
    {
        builder.ToTable("productDeliver_size");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ProductDeliverId).HasColumnName("productDeliver_id");
        builder.Property(x => x.ProductSizeId).HasColumnName("productSize_id");
    }
}
