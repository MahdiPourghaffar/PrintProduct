using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductMaterial : IEntity<int>
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int MaterialId { get; private set; }
    public string? Name { get; private set; }
    public bool IsJeld { get; private set; }
    public bool Required { get; private set; }
    public bool IsCustomCirculation { get; private set; }
    public bool IsCombinedMaterial { get; private set; }
    public int? Weight { get; private set; }
    public Product Product { get; private set; } = null!;
    public ICollection<ProductMaterialAttribute> Attributes { get; private set; } = new List<ProductMaterialAttribute>();
    private ProductMaterial() { }
    internal ProductMaterial(
        int materialId,
        string? name,
        bool isJeld,
        bool required,
        bool isCustomCirculation,
        bool isCombinedMaterial,
        int? weight)
    {
        if (materialId <= 0)
            throw new DomainException("MaterialId is invalid");

        if (weight is < 0)
            throw new DomainException("Weight cannot be negative");

        MaterialId = materialId;
        Name = name;
        IsJeld = isJeld;
        Required = required;
        IsCustomCirculation = isCustomCirculation;
        IsCombinedMaterial = isCombinedMaterial;
        Weight = weight;
    }

    // -----------------------
    // Domain Behaviors
    // -----------------------

    public void ChangeName(string? name)
    {
        Name = name;
    }

    public void ChangeWeight(int? weight)
    {
        if (weight is < 0)
            throw new DomainException("Weight cannot be negative");

        Weight = weight;
    }

    public void SetRequired(bool required)
    {
        Required = required;
    }

    public void MarkAsJeld(bool isJeld)
    {
        IsJeld = isJeld;
    }

    // -----------------------
    // Attribute management
    // -----------------------

    public void AddAttribute(int materialAttributeId)
    {
        if (materialAttributeId <= 0)
            throw new DomainException("MaterialAttributeId is invalid");

        Attributes.Add(new ProductMaterialAttribute(this, materialAttributeId));
    }

    public void RemoveAttribute(int materialAttributeId)
    {
        var attribute = Attributes
            .FirstOrDefault(x => x.MaterialAttributeId == materialAttributeId);

        if (attribute == null)
            throw new DomainException("MaterialAttribute not found");

        Attributes.Remove(attribute);
    }

    public void Update(string? name, bool required, bool isCustomCirculation, bool isCombinedMaterial, int? weight)
    {
        Name = name;
        Required = required;
        IsCustomCirculation = isCustomCirculation;
        IsCombinedMaterial = isCombinedMaterial;
        Weight = weight;
    }
}
