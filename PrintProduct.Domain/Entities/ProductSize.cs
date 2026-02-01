using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductSize : IEntity<int>
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public string Name { get; private set; } = null!;
    public int? SheetCount { get; private set; }
    public int? SheetDimensionId { get; private set; }
    public Product Product { get; private set; } = null!;
    private ProductSize() { }

    internal ProductSize(
        int productId,
        string name,
        float length,
        float width,
        int sheetDimensionId,
        int? sheetCount = null)
    {
        if (productId <= 0)
            throw new DomainException("ProductId is invalid");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name cannot be empty");

        if (length <= 0)
            throw new DomainException("Length must be greater than zero");

        if (width <= 0)
            throw new DomainException("Width must be greater than zero");

        if (sheetDimensionId <= 0)
            throw new DomainException("SheetDimensionId is invalid");

        if (sheetCount.HasValue && sheetCount <= 0)
            throw new DomainException("SheetCount must be greater than zero if specified");

        ProductId = productId;
        Name = name;
        Length = length;
        Width = width;
        SheetDimensionId = sheetDimensionId;
        SheetCount = sheetCount;
    }

    // -----------------------
    // Domain behaviors
    // -----------------------
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name cannot be empty");

        Name = name;
    }

    public void SetDimensions(float length, float width)
    {
        if (length <= 0 || width <= 0)
            throw new DomainException("Length and Width must be greater than zero");

        Length = length;
        Width = width;
    }

    public void SetSheetCount(int? sheetCount)
    {
        if (sheetCount.HasValue && sheetCount <= 0)
            throw new DomainException("SheetCount must be greater than zero if specified");

        SheetCount = sheetCount;
    }

    public void SetSheetDimensionId(int sheetDimensionId)
    {
        if (sheetDimensionId <= 0)
            throw new DomainException("SheetDimensionId is invalid");

        SheetDimensionId = sheetDimensionId;
    }


    internal void Update(string name, float length, float width, int sheetDimensionId, int? sheetCount = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Size name is required");

        Name = name;
        Length = length;
        Width = width;
        SheetDimensionId = sheetDimensionId;
        SheetCount = sheetCount;
    }
}
