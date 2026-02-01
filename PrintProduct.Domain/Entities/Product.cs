using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class Product : IEntity<int>, IAggregateRoot
{
    public int Id { get; private set; }
    public int ProductGroupId { get; private set; }
    public int WorkTypeId { get; private set; }
    public byte ProductType { get; private set; }
    public string? Circulation { get; private set; }
    public string? CopyCount { get; private set; }
    public string? PageCount { get; private set; }
    public byte PrintSide { get; private set; }
    public bool IsDeleted { get; private set; }
    public bool IsCalculatePrice { get; private set; }
    public bool IsCustomCirculation { get; private set; }
    public bool IsCustomSize { get; private set; }
    public bool IsCustomPage { get; private set; }
    public int? MinCirculation { get; private set; }
    public int? MaxCirculation { get; private set; }
    public int? MinPage { get; private set; }
    public int? MaxPage { get; private set; }
    public double? MinWidth { get; private set; }
    public double? MaxWidth { get; private set; }
    public double? MinLength { get; private set; }
    public double? MaxLength { get; private set; }
    public int SheetDimensionId { get; private set; }
    public string FileExtension { get; private set; } = null!;
    public bool IsCmyk { get; private set; }
    public double CutMargin { get; private set; }
    public double PrintMargin { get; private set; }
    public bool IsCheckFile { get; private set; }
    public ProductJeld? Jeld { get; private set; }
    // Navigation
    public ICollection<ProductSize> Sizes { get; private set; } = [];
    public ICollection<ProductMaterial> Materials { get; private set; } = [];
    public ICollection<ProductPrintKind> PrintKinds { get; private set; } = [];
    public ICollection<ProductAdt> Adts { get; private set; } = [];
    public ICollection<ProductDeliver> Delivers { get; private set; } = [];
    public ICollection<ProductPrice> Prices { get; private set; } = [];
    private Product() { }

    private Product(
        int productGroupId,
        int workTypeId,
        byte productType,
        byte printSide,
        int sheetDimensionId,
        string fileExtension,
        bool isCmyk,
        bool isCalculatePrice)
    {
        ProductGroupId = productGroupId;
        WorkTypeId = workTypeId;
        ProductType = productType;
        PrintSide = printSide;
        SheetDimensionId = sheetDimensionId;

        FileExtension = fileExtension;
        IsCmyk = isCmyk;

        IsCalculatePrice = isCalculatePrice;
        IsDeleted = false;
    }

    public static Product Create(
        int productGroupId,
        int workTypeId,
        byte productType,
        byte printSide,
        int sheetDimensionId,
        string fileExtension,
        bool isCmyk = false,
        bool isCalculatePrice = true)
    {
        if (productGroupId <= 0)
            throw new DomainException("ProductGroupId is invalid");

        if (workTypeId <= 0)
            throw new DomainException("WorkTypeId is invalid");

        if (string.IsNullOrWhiteSpace(fileExtension))
            throw new DomainException("FileExtension is required");

        return new Product(
            productGroupId,
            workTypeId,
            productType,
            printSide,
            sheetDimensionId,
            fileExtension,
            isCmyk,
            isCalculatePrice);
    }

    // -----------------------
    // Domain Behaviors
    // -----------------------

    public void AddPrintKind(
        int printKindId,
        string? name,
        bool required,
        bool isJeld)
    {
        if (PrintKinds.Any(pk => pk.PrintKindId == printKindId))
            throw new DomainException("PrintKind already exists for this product");

        var printKind = new ProductPrintKind(
            printKindId,
            name,
            required,
            isJeld
        );

        PrintKinds.Add(printKind);
    }

    public void AddPrice(
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
        var productPrice = new ProductPrice(
            circulation,
            price,
            isDoubleSided,
            productSizeId,
            productMaterialId,
            productPrintKindId,
            productMaterialAttributeId,
            pageCount,
            copyCount,
            isJeld
        );

        Prices.Add(productPrice);
    }


    public void ConfigureCirculation(
        string? circulation,
        int? min,
        int? max,
        bool isCustom)
    {
        Circulation = circulation;
        MinCirculation = min;
        MaxCirculation = max;
        IsCustomCirculation = isCustom;
    }

    public void ConfigurePage(
        string? pageCount,
        int? min,
        int? max,
        bool isCustom)
    {
        PageCount = pageCount;
        MinPage = min;
        MaxPage = max;
        IsCustomPage = isCustom;
    }

    public void ConfigureCopy(string? copyCount)
    {
        CopyCount = copyCount;
    }

    public void ConfigureSize(
        double? minWidth,
        double? maxWidth,
        double? minLength,
        double? maxLength,
        bool isCustom)
    {
        MinWidth = minWidth;
        MaxWidth = maxWidth;
        MinLength = minLength;
        MaxLength = maxLength;
        IsCustomSize = isCustom;
    }

    public void ConfigurePrintMargins(double cutMargin, double printMargin)
    {
        CutMargin = cutMargin;
        PrintMargin = printMargin;
    }

    public void EnableFileCheck()
    {
        IsCheckFile = true;
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
    }

    // -----------------------
    // Child management
    // -----------------------


    public void AddSize(
        string name,
        float length,
        float width,
        int sheetDimensionId,
        int? sheetCount = null)
    {
        if (Sizes.Any(s => s.Name == name))
            throw new DomainException("A size with this name already exists");

        var size = new ProductSize(
            this.Id,
            name,
            length,
            width,
            sheetDimensionId,
            sheetCount
        );

        Sizes.Add(size);
    }


    public void AddAdt(
        int adtId,
        bool required,
        byte? side,
        int? count,
        bool isJeld)
    {
        Adts.Add(new ProductAdt(
            adtId,
            required,
            side,
            count,
            isJeld));
    }
    public void AddDeliver(
        string name,
        bool isIncreased,
        int startCirculation,
        int endCirculation,
        byte printSide,
        float price,
        byte calcType)
    {
        Delivers.Add(new ProductDeliver(
            name,
            isIncreased,
            startCirculation,
            endCirculation,
            printSide,
            price,
            calcType));
    }

    public void SetJeld(
        byte printSide,
        string fileExtension,
        bool isCmyk,
        float? cutMargin,
        float? printMargin,
        bool isCheckFile)
    {
        Jeld = new ProductJeld(
            printSide,
            fileExtension,
            isCmyk,
            cutMargin,
            printMargin,
            isCheckFile);
    }

    public void AddMaterial(
        int materialId,
        string? name,
        bool isJeld,
        bool required,
        bool isCustomCirculation,
        bool isCombinedMaterial,
        int? weight)
    {
        Materials.Add(new ProductMaterial(
            materialId,
            name,
            isJeld,
            required,
            isCustomCirculation,
            isCombinedMaterial,
            weight));
    }

    // -----------------------
    // Update Aggregate Root
    // -----------------------

    public void Update(
        int productGroupId,
        int workTypeId,
        byte productType,
        byte printSide,
        int sheetDimensionId,
        string fileExtension,
        bool isCmyk,
        bool isCalculatePrice)
    {
        if (productGroupId <= 0)
            throw new DomainException("ProductGroupId is invalid");

        if (workTypeId <= 0)
            throw new DomainException("WorkTypeId is invalid");

        if (string.IsNullOrWhiteSpace(fileExtension))
            throw new DomainException("FileExtension is required");

        ProductGroupId = productGroupId;
        WorkTypeId = workTypeId;
        ProductType = productType;
        PrintSide = printSide;
        SheetDimensionId = sheetDimensionId;
        FileExtension = fileExtension;
        IsCmyk = isCmyk;
        IsCalculatePrice = isCalculatePrice;
    }

    // -----------------------
    // Update Children
    // -----------------------

    // Update existing Size
    public void UpdateSize(int sizeId, string name, float length, float width, int sheetDimensionId, int? sheetCount = null)
    {
        var size = Sizes.FirstOrDefault(s => s.Id == sizeId);
        if (size == null)
            throw new DomainException("Size not found");

        size.Update(name, length, width, sheetDimensionId, sheetCount);
    }

    // Update existing Material
    public void UpdateMaterial(int materialId, string? name, bool required, bool isCustomCirculation, bool isCombinedMaterial, int? weight)
    {
        var material = Materials.FirstOrDefault(m => m.MaterialId == materialId);
        if (material == null)
            throw new DomainException("Material not found");

        material.Update(name, required, isCustomCirculation, isCombinedMaterial, weight);
    }

    // Update existing Adt
    public void UpdateAdt(int adtId, bool required, byte? side, int? count, bool isJeld)
    {
        var adt = Adts.FirstOrDefault(a => a.AdtId == adtId);
        if (adt == null)
            throw new DomainException("Adt not found");

        adt.Update(required, side, count, isJeld);
    }

    // Update existing Deliver
    public void UpdateDeliver(int deliverId, string name, bool isIncreased, int startCirculation, int endCirculation, byte printSide, float price, byte calcType)
    {
        var deliver = Delivers.FirstOrDefault(d => d.Id == deliverId);
        if (deliver == null)
            throw new DomainException("Deliver not found");

        deliver.Update(name, isIncreased, startCirculation, endCirculation, printSide, price, calcType);
    }

    // Update Jeld
    public void UpdateJeld(byte printSide, string fileExtension, bool isCmyk, float? cutMargin, float? printMargin, bool isCheckFile)
    {
        if (Jeld == null)
            throw new DomainException("Jeld is not set");

        Jeld.Update(printSide, fileExtension, isCmyk, cutMargin, printMargin, isCheckFile);
    }

    // -----------------------
    // Update Configurations
    // -----------------------

    public void UpdateCirculation(string? circulation, int? min, int? max, bool isCustom)
    {
        Circulation = circulation;
        MinCirculation = min;
        MaxCirculation = max;
        IsCustomCirculation = isCustom;
    }

    public void UpdatePage(string? pageCount, int? min, int? max, bool isCustom)
    {
        PageCount = pageCount;
        MinPage = min;
        MaxPage = max;
        IsCustomPage = isCustom;
    }

    public void UpdateSizeConfiguration(double? minWidth, double? maxWidth, double? minLength, double? maxLength, bool isCustom)
    {
        MinWidth = minWidth;
        MaxWidth = maxWidth;
        MinLength = minLength;
        MaxLength = maxLength;
        IsCustomSize = isCustom;
    }

    public void UpdatePrintMargins(double cutMargin, double printMargin)
    {
        CutMargin = cutMargin;
        PrintMargin = printMargin;
    }

}
