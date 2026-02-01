using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductAdtType : IEntity<int>
{
    public int Id { get; private set; }
    public int ProductAdtId { get; private set; }
    public int AdtTypeId { get; private set; }
    public ProductAdt ProductAdt { get; private set; } = null!;

    private ProductAdtType() { }

    internal ProductAdtType(int adtTypeId)
    {
        if (adtTypeId <= 0)
            throw new DomainException("AdtTypeId is invalid");

        AdtTypeId = adtTypeId;
    }
}