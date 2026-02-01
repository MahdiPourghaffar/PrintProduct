using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductAdtPrice : IEntity<int>
{
    public int Id { get; private set; }
    public int ProductAdtId { get; private set; }
    public int ProductPriceId { get; private set; }
    public int ProductAdtTypeId { get; private set; }
    public float Price { get; private set; }
    public ProductAdt ProductAdt { get; private set; } = null!;
    private ProductAdtPrice() { }

    internal ProductAdtPrice(
        int productAdtId,
        int productPriceId,
        int productAdtTypeId,
        float price)
    {
        if (productAdtId <= 0)
            throw new DomainException("ProductAdtId is invalid");

        if (productPriceId <= 0)
            throw new DomainException("ProductPriceId is invalid");

        if (productAdtTypeId <= 0)
            throw new DomainException("ProductAdtTypeId is invalid");

        if (price < 0)
            throw new DomainException("Price cannot be negative");

        ProductAdtId = productAdtId;
        ProductPriceId = productPriceId;
        ProductAdtTypeId = productAdtTypeId;
        Price = price;
    }

    // -----------------------
    // Domain behaviors
    // -----------------------
    public void ChangePrice(float price)
    {
        if (price < 0)
            throw new DomainException("Price cannot be negative");

        Price = price;
    }

    public void SetProductAdtType(int productAdtTypeId)
    {
        if (productAdtTypeId <= 0)
            throw new DomainException("ProductAdtTypeId is invalid");

        ProductAdtTypeId = productAdtTypeId;
    }
}