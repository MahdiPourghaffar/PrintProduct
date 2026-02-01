using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductPrintKind : IEntity<int>
{
    public int Id { get; private set; }
    public int PrintKindId { get; private set; }
    public string? Name { get; private set; }
    public bool Required { get; private set; }
    public bool IsJeld { get; private set; }

    private ProductPrintKind() { } // EF

    internal ProductPrintKind(
        int printKindId,
        string? name,
        bool required,
        bool isJeld)
    {
        if (printKindId <= 0)
            throw new DomainException("PrintKindId is invalid");

        PrintKindId = printKindId;
        Name = name;
        Required = required;
        IsJeld = isJeld;
    }

    internal void Update(string? name, bool required, bool isJeld)
    {
        Name = name;
        Required = required;
        IsJeld = isJeld;
    }

}