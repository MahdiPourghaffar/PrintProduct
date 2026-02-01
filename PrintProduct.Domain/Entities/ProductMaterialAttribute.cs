using PrintProduct.Domain.Common;
using PrintProduct.Domain.Entities;
using PrintProduct.Domain.Exceptions;

public class ProductMaterialAttribute : IEntity<int>
{
    public int Id { get; private set; }
    public int ProductMaterialId { get; private set; }
    public int MaterialAttributeId { get; private set; }
    public ProductMaterial ProductMaterial { get; private set; } = null!;
    private ProductMaterialAttribute() { }

    internal ProductMaterialAttribute(ProductMaterial productMaterial, int materialAttributeId)
    {
        ProductMaterial = productMaterial ?? throw new DomainException("ProductMaterial cannot be null");
        ProductMaterialId = productMaterial.Id;

        if (materialAttributeId <= 0)
            throw new DomainException("MaterialAttributeId is invalid");

        MaterialAttributeId = materialAttributeId;
    }
}