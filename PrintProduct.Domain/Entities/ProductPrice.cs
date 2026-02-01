using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductPrice : IEntity<int>
{
    public int Id { get; private set; }

    public int Circulation { get; private set; }
    public double Price { get; private set; }
    public bool IsDoubleSided { get; private set; }

    public int ProductSizeId { get; private set; }
    public int ProductMaterialId { get; private set; }
    public int ProductPrintKindId { get; private set; }

    public int? ProductMaterialAttributeId { get; private set; }
    public int? PageCount { get; private set; }
    public int? CopyCount { get; private set; }
    public bool IsJeld { get; private set; }

    private ProductPrice() { }

    internal ProductPrice(
        int circulation,
        double price,
        bool isDoubleSided,
        int productSizeId,
        int productMaterialId,
        int productPrintKindId,
        int? productMaterialAttributeId,
        int? pageCount,
        int? copyCount,
        bool isJeld)
    {
        Validate(circulation, price, pageCount, copyCount);

        Circulation = circulation;
        Price = price;
        IsDoubleSided = isDoubleSided;

        ProductSizeId = productSizeId;
        ProductMaterialId = productMaterialId;
        ProductPrintKindId = productPrintKindId;
        ProductMaterialAttributeId = productMaterialAttributeId;

        PageCount = pageCount;
        CopyCount = copyCount;
        IsJeld = isJeld;
    }

    private static void Validate(
        int circulation,
        double price,
        int? pageCount,
        int? copyCount)
    {
        if (circulation <= 0)
            throw new DomainException("Circulation must be greater than zero");

        if (price < 0)
            throw new DomainException("Price cannot be negative");

        if (pageCount.HasValue && pageCount <= 0)
            throw new DomainException("PageCount must be greater than zero");

        if (copyCount.HasValue && copyCount <= 0)
            throw new DomainException("CopyCount must be greater than zero");
    }

    // -----------------------
    // Domain Behaviors
    // -----------------------

    public void ChangePrice(double price)
    {
        if (price < 0)
            throw new DomainException("Price cannot be negative");

        Price = price;
    }

    public void ChangeCirculation(int circulation)
    {
        if (circulation <= 0)
            throw new DomainException("Circulation must be greater than zero");

        Circulation = circulation;
    }

    public void SetDoubleSided(bool isDoubleSided) => IsDoubleSided = isDoubleSided;

    public void SetPageCount(int? pageCount)
    {
        if (pageCount.HasValue && pageCount <= 0)
            throw new DomainException("Invalid PageCount");

        PageCount = pageCount;
    }

    public void SetCopyCount(int? copyCount)
    {
        if (copyCount.HasValue && copyCount <= 0)
            throw new DomainException("Invalid CopyCount");

        CopyCount = copyCount;
    }

    public void SetMaterialAttribute(int? attributeId)
    {
        ProductMaterialAttributeId = attributeId;
    }

    public void SetJeld(bool isJeld) => IsJeld = isJeld;
}
