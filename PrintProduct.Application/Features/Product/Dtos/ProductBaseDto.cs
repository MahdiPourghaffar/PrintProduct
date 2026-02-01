namespace PrintProduct.Application.Features.Product.Dtos;

public class ProductBaseDto
{
    public int ProductGroupId { get; set; }
    public int WorkTypeId { get; set; }
    public byte ProductType { get; set; }
    public string? Circulation { get; set; }
    public string? CopyCount { get; set; }
    public string? PageCount { get; set; }
    public byte PrintSide { get; set; }
    public bool IsCalculatePrice { get; set; }
    public bool IsCustomCirculation { get; set; }
    public bool IsCustomSize { get; set; }
    public bool IsCustomPage { get; set; }
    public int? MinCirculation { get; set; }
    public int? MaxCirculation { get; set; }
    public int? MinPage { get; set; }
    public int? MaxPage { get; set; }
    public double? MinWidth { get; set; }
    public double? MaxWidth { get; set; }
    public double? MinLength { get; set; }
    public double? MaxLength { get; set; }
    public int SheetDimensionId { get; set; }
    public string FileExtension { get; set; } = null!;
    public bool IsCmyk { get; set; }
    public double CutMargin { get; set; }
    public double PrintMargin { get; set; }
    public bool IsCheckFile { get; set; }
    public bool IsDeleted { get; set; }
    public ProductJeldDto? Jeld { get; set; }
    public List<ProductSizeDto> Sizes { get; set; } = [];
    public List<ProductMaterialDto> Materials { get; set; } = [];
    public List<ProductPrintKindDto> PrintKinds { get; set; } = [];
    public List<ProductAdtDto> Adts { get; set; } = [];
    public List<ProductDeliverDto> Delivers { get; set; } = [];
    public List<ProductPriceDto> Prices { get; set; } = [];
}


public class ProductAdtDto
{
    public int AdtId { get; set; }
    public bool Required { get; set; }
    public byte? Side { get; set; }
    public int? Count { get; set; }
    public bool IsJeld { get; set; }
    public List<ProductAdtTypeDto> Types { get; set; } = [];
    public List<ProductAdtPriceDto> Prices { get; set; } = [];
}
public class ProductAdtPriceDto
{
    public int Circulation { get; set; }
    public double Price { get; set; }
}
public class ProductAdtTypeDto
{
    public string Name { get; set; } = null!;
}
public class ProductMaterialDto
{
    public int MaterialId { get; set; }
    public string? Name { get; set; }
    public bool IsJeld { get; set; }
    public bool Required { get; set; }
    public bool IsCustomCirculation { get; set; }
    public bool IsCombinedMaterial { get; set; }
    public int? Weight { get; set; }

    public List<ProductMaterialAttributeDto> Attributes { get; set; } = new();
}
public class ProductMaterialAttributeDto
{
    public string Name { get; set; } = null!;
}

public class ProductSizeDto
{
    public string Name { get; set; } = null!;            
    public float Length { get; set; }               
    public float Width { get; set; }               
    public int SheetDimensionId { get; set; }          
    public int? SheetCount { get; set; }         
}


public class ProductDeliverDto
{
    public string Name { get; set; } = null!;
    public bool IsIncreased { get; set; }
    public int StartCirculation { get; set; }     
    public int EndCirculation { get; set; }         
    public float Price { get; set; }  
    public byte PrintSide { get; set; }
    public byte CalcType { get; set; }

    public List<ProductDeliverSizeDto> Sizes { get; set; } = new();
}
public class ProductDeliverSizeDto
{
    public int StartCirculation { get; set; }
    public int EndCirculation { get; set; }
    public float Price { get; set; }
}
public class ProductJeldDto
{
    public byte PrintSide { get; set; }
    public string FileExtension { get; set; } = null!;
    public bool IsCmyk { get; set; }
    public float? CutMargin { get; set; }
    public float? PrintMargin { get; set; }
    public bool IsCheckFile { get; set; }
}
public class ProductPrintKindDto
{
    public int PrintKindId { get; set; }
    public string? Name { get; set; }
    public bool Required { get; set; }
    public bool IsJeld { get; set; }
}

public class ProductPriceDto
{
    public int Circulation { get; set; }
    public double Price { get; set; }
    public bool IsDoubleSided { get; set; }

    public int ProductSizeId { get; set; }
    public int ProductMaterialId { get; set; }
    public int ProductPrintKindId { get; set; }

    public int? ProductMaterialAttributeId { get; set; }
    public int? PageCount { get; set; }
    public int? CopyCount { get; set; }
    public bool IsJeld { get; set; }
}

