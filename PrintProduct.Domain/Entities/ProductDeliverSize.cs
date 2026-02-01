using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductDeliverSize : IEntity<int>
{
    public int Id { get; private set; }
    public int ProductDeliverId { get; private set; }
    public int ProductSizeId { get; private set; }
    private ProductDeliverSize() { }

    internal ProductDeliverSize(int productSizeId)
    {
        if (productSizeId <= 0)
            throw new DomainException("ProductSizeId is invalid");

        ProductSizeId = productSizeId;
    }
}