using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductDeliver : IEntity<int>
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public string Name { get; private set; } = null!;
    public bool IsIncreased { get; private set; }
    public int StartCirculation { get; private set; }
    public int EndCirculation { get; private set; }
    public byte PrintSide { get; private set; }
    public float Price { get; private set; }
    public byte CalcType { get; private set; }
    public Product Product { get; private set; } = null!;
    public ICollection<ProductDeliverSize> Sizes { get; private set; } = new List<ProductDeliverSize>();
    private ProductDeliver() { }
    internal ProductDeliver(
        string name,
        bool isIncreased,
        int startCirculation,
        int endCirculation,
        byte printSide,
        float price,
        byte calcType)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Deliver name is required");

        if (startCirculation < 0 || endCirculation < 0)
            throw new DomainException("Circulation cannot be negative");

        if (startCirculation > endCirculation)
            throw new DomainException("StartCirculation cannot be greater than EndCirculation");

        if (price < 0)
            throw new DomainException("Price cannot be negative");

        Name = name;
        IsIncreased = isIncreased;
        StartCirculation = startCirculation;
        EndCirculation = endCirculation;
        PrintSide = printSide;
        Price = price;
        CalcType = calcType;
    }

    // -----------------------
    // Domain Behaviors
    // -----------------------

    public void ChangePrice(float price)
    {
        if (price < 0)
            throw new DomainException("Price cannot be negative");

        Price = price;
    }

    public void ChangeCirculationRange(int start, int end)
    {
        if (start < 0 || end < 0)
            throw new DomainException("Circulation cannot be negative");

        if (start > end)
            throw new DomainException("StartCirculation cannot be greater than EndCirculation");

        StartCirculation = start;
        EndCirculation = end;
    }

    public void AddSize(int productSizeId)
    {
        if (productSizeId <= 0)
            throw new DomainException("ProductSizeId is invalid");

        Sizes.Add(new ProductDeliverSize(productSizeId));
    }

    internal void Update(string name, bool isIncreased, int startCirculation, int endCirculation, byte printSide, float price, byte calcType)
    {
        Name = name;
        IsIncreased = isIncreased;
        StartCirculation = startCirculation;
        EndCirculation = endCirculation;
        PrintSide = printSide;
        Price = price;
        CalcType = calcType;
    }
}
