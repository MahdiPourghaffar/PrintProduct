using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintProduct.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintProduct.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");

        builder.Property(x => x.ProductGroupId).HasColumnName("productGroup_id");
        builder.Property(x => x.WorkTypeId).HasColumnName("workType_Id");
        builder.Property(x => x.ProductType).HasColumnName("productType");

        builder.Property(x => x.Circulation)
            .HasColumnName("Circulation")
            .HasColumnType("nvarchar(max)")
            .UseCollation("SQL_Latin1_General_CP1_CI_AI")
            .IsRequired(false);

        builder.Property(x => x.CopyCount)
            .HasColumnName("CopyCount")
            .HasColumnType("nvarchar(max)")
            .UseCollation("SQL_Latin1_General_CP1_CI_AI")
            .IsRequired(false);

        builder.Property(x => x.PageCount)
            .HasColumnName("pageCount")
            .HasColumnType("nvarchar(max)")
            .UseCollation("SQL_Latin1_General_CP1_CI_AI")
            .IsRequired(false);

        builder.Property(x => x.PrintSide).HasColumnName("print_side");

        builder.Property(x => x.IsDeleted)
            .HasColumnName("IsDelete")
            .HasDefaultValue(0);

        builder.Property(x => x.IsCalculatePrice)
            .HasColumnName("IsCalculatePrice")
            .HasDefaultValue(1);

        builder.Property(x => x.IsCustomCirculation)
            .HasColumnName("IsCustomCirculation")
            .HasDefaultValue(0);

        builder.Property(x => x.IsCustomSize)
            .HasColumnName("IsCustomSize")
            .HasDefaultValue(0);

        builder.Property(x => x.IsCustomPage)
            .HasColumnName("IsCustomPage")
            .HasDefaultValue(0);

        builder.Property(x => x.MinCirculation)
            .HasColumnName("minCirculation")
            .IsRequired(false);

        builder.Property(x => x.MaxCirculation)
            .HasColumnName("maxCirculation")
            .IsRequired(false);

        builder.Property(x => x.MinPage)
            .HasColumnName("minPage")
            .IsRequired(false);

        builder.Property(x => x.MaxPage)
            .HasColumnName("maxPage")
            .IsRequired(false);

        builder.Property(x => x.MinWidth)
            .HasColumnName("minWidth")
            .IsRequired(false);

        builder.Property(x => x.MaxWidth)
            .HasColumnName("maxWidth")
            .IsRequired(false);

        builder.Property(x => x.MinLength)
            .HasColumnName("minLength")
            .IsRequired(false);

        builder.Property(x => x.MaxLength)
            .HasColumnName("maxLength")
            .IsRequired(false);

        builder.Property(x => x.SheetDimensionId).HasColumnName("sheetDimension_id");

        builder.Property(x => x.FileExtension)
            .HasColumnName("fileExtension")
            .HasColumnType("nvarchar(max)")
            .UseCollation("SQL_Latin1_General_CP1_CI_AI");

        builder.Property(x => x.IsCmyk)
            .HasColumnName("IsCmyk")
            .HasDefaultValue(0);

        builder.Property(x => x.CutMargin)
            .HasColumnName("cutMargin")
            .HasDefaultValue(0);

        builder.Property(x => x.PrintMargin)
            .HasColumnName("printMargin")
            .HasDefaultValue(0);

        builder.Property(x => x.IsCheckFile).HasColumnName("isCheckFile");
    }
}


