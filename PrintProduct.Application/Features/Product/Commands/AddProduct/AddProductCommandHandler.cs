using MediatR;
using PrintProduct.Application.Contracts.Persistence;
using PrintProduct.Domain.Entities;

namespace PrintProduct.Application.Features.Product.Commands.AddProduct;
public class AddProductCommandHandler(IProductRepository repository)
        : IRequestHandler<AddProductCommand, int>
{
    public async Task<int> Handle(
        AddProductCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Product;

        // -----------------------
        // 1. Create Aggregate Root
        // -----------------------
        var product = Domain.Entities.Product.Create(
            dto.ProductGroupId,
            dto.WorkTypeId,
            dto.ProductType,
            dto.PrintSide,
            dto.SheetDimensionId,
            dto.FileExtension,
            dto.IsCmyk,
            dto.IsCalculatePrice
        );

        // -----------------------
        // 2. Configure domain rules
        // -----------------------

        product.ConfigureCirculation(
            dto.Circulation,
            dto.MinCirculation,
            dto.MaxCirculation,
            dto.IsCustomCirculation
        );

        product.ConfigurePage(
            dto.PageCount,
            dto.MinPage,
            dto.MaxPage,
            dto.IsCustomPage
        );

        product.ConfigureCopy(dto.CopyCount);

        product.ConfigureSize(
            dto.MinWidth,
            dto.MaxWidth,
            dto.MinLength,
            dto.MaxLength,
            dto.IsCustomSize
        );

        product.ConfigurePrintMargins(
            dto.CutMargin,
            dto.PrintMargin
        );

        if (dto.IsCheckFile)
            product.EnableFileCheck();

        // -----------------------
        // 3. Jeld
        // -----------------------
        if (dto.Jeld is not null)
        {
            product.SetJeld(
                dto.Jeld.PrintSide,
                dto.Jeld.FileExtension,
                dto.Jeld.IsCmyk,
                dto.Jeld.CutMargin,
                dto.Jeld.PrintMargin,
                dto.Jeld.IsCheckFile
            );
        }

        // -----------------------
        // 4. Materials
        // -----------------------
        if (dto.Materials is not null)
        {
            foreach (var material in dto.Materials)
            {
                product.AddMaterial(
                    material.MaterialId,
                    material.Name,
                    material.IsJeld,
                    material.Required,
                    material.IsCustomCirculation,
                    material.IsCombinedMaterial,
                    material.Weight
                );
            }
        }

        // -----------------------
        // 5. Sizes
        // -----------------------
        foreach (var sizeDto in request.Product.Sizes)
        {
            product.AddSize(
                sizeDto.Name,
                sizeDto.Length,
                sizeDto.Width,
                sizeDto.SheetDimensionId,
                sizeDto.SheetCount
            );
        }

        // -----------------------
        // 6. Adts
        // -----------------------
        if (dto.Adts is not null)
        {
            foreach (var adt in dto.Adts)
            {
                product.AddAdt(
                    adt.AdtId,
                    adt.Required,
                    adt.Side,
                    adt.Count,
                    adt.IsJeld
                );
            }
        }

        // -----------------------
        // 7. Delivers
        // -----------------------
        if (dto.Delivers is not null)
        {
            foreach (var deliver in dto.Delivers)
            {
                product.AddDeliver(
                    deliver.Name,
                    deliver.IsIncreased,
                    deliver.StartCirculation,
                    deliver.EndCirculation,
                    deliver.PrintSide,
                    deliver.Price,
                    deliver.CalcType
                );
            }
        }

        
        // -----------------------
        // 8. Prices
        // -----------------------
        foreach (var price in dto.Prices)
        {
            product.AddPrice(
                price.Circulation,
                price.Price,
                price.IsDoubleSided,
                price.ProductSizeId,
                price.ProductMaterialId,
                price.ProductPrintKindId,
                price.ProductMaterialAttributeId,
                price.PageCount,
                price.CopyCount,
                price.IsJeld
            );
        }



        // -----------------------
        // 9. Persist Aggregate
        // -----------------------
        await repository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}

