using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductAdt : IEntity<int>
{
    public int Id { get; private set; }
    public int AdtId { get; private set; }
    public int ProductId { get; private set; }
    public bool Required { get; private set; }
    public byte? Side { get; private set; }
    public int? Count { get; private set; }
    public bool IsJeld { get; private set; }
    public Product Product { get; private set; } = null!;
    public ICollection<ProductAdtType> Types { get; private set; } = new List<ProductAdtType>();
    public ICollection<ProductAdtPrice> Prices { get; private set; } = new List<ProductAdtPrice>();

    private ProductAdt() { }
    internal ProductAdt(
        int adtId,
        bool required,
        byte? side,
        int? count,
        bool isJeld)
    {
        if (adtId <= 0)
            throw new DomainException("AdtId is invalid");

        AdtId = adtId;
        Required = required;
        Side = side;
        Count = count;
        IsJeld = isJeld;
    }

    // -----------------------
    // Domain Behaviors
    // -----------------------

    public void ChangeRequirement(bool required)
    {
        Required = required;
    }

    public void ChangeSide(byte? side)
    {
        Side = side;
    }

    public void ChangeCount(int? count)
    {
        if (count is < 0)
            throw new DomainException("Count cannot be negative");

        Count = count;
    }

    public void MarkAsJeld()
    {
        IsJeld = true;
    }

    // -----------------------
    // Child management
    // -----------------------

    public void AddType(int adtTypeId)
    {
        if (adtTypeId <= 0)
            throw new DomainException("AdtTypeId is invalid");

        Types.Add(new ProductAdtType(adtTypeId));
    }

    public void AddPrice(
        int productPriceId,
        int productAdtTypeId,
        float price)
    {
        if (price < 0)
            throw new DomainException("Price cannot be negative");

        Prices.Add(new ProductAdtPrice(
            productAdtId: this.Id,
            productPriceId: productPriceId,
            productAdtTypeId: productAdtTypeId,
            price: price));
    }

    internal void Update(bool required, byte? side, int? count, bool isJeld)
    {
        Required = required;
        Side = side;
        Count = count;
        IsJeld = isJeld;
    }
}
