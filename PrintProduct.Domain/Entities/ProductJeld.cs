using PrintProduct.Domain.Common;
using PrintProduct.Domain.Exceptions;

namespace PrintProduct.Domain.Entities;

public class ProductJeld : IEntity<int>
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public byte PrintSide { get; private set; }
    public string FileExtension { get; private set; } = null!;
    public bool IsCmyk { get; private set; }
    public float? CutMargin { get; private set; }
    public float? PrintMargin { get; private set; }
    public bool IsCheckFile { get; private set; }
    private ProductJeld() { }
    internal ProductJeld(
        byte printSide,
        string fileExtension,
        bool isCmyk,
        float? cutMargin,
        float? printMargin,
        bool isCheckFile)
    {
        if (string.IsNullOrWhiteSpace(fileExtension))
            throw new DomainException("FileExtension is required");

        if (cutMargin is < 0)
            throw new DomainException("CutMargin cannot be negative");

        if (printMargin is < 0)
            throw new DomainException("PrintMargin cannot be negative");

        PrintSide = printSide;
        FileExtension = fileExtension;
        IsCmyk = isCmyk;
        CutMargin = cutMargin;
        PrintMargin = printMargin;
        IsCheckFile = isCheckFile;
    }

    // -----------------------
    // Domain Behaviors
    // -----------------------

    public void ChangeMargins(float? cutMargin, float? printMargin)
    {
        if (cutMargin is < 0 || printMargin is < 0)
            throw new DomainException("Margins cannot be negative");

        CutMargin = cutMargin;
        PrintMargin = printMargin;
    }

    public void ChangeFileSettings(string fileExtension, bool isCmyk, bool isCheckFile)
    {
        if (string.IsNullOrWhiteSpace(fileExtension))
            throw new DomainException("FileExtension is required");

        FileExtension = fileExtension;
        IsCmyk = isCmyk;
        IsCheckFile = isCheckFile;
    }

    internal void Update(byte printSide, string fileExtension, bool isCmyk, float? cutMargin, float? printMargin, bool isCheckFile)
    {
        if (string.IsNullOrWhiteSpace(fileExtension))
            throw new DomainException("FileExtension is required");

        PrintSide = printSide;
        FileExtension = fileExtension;
        IsCmyk = isCmyk;
        CutMargin = cutMargin;
        PrintMargin = printMargin;
        IsCheckFile = isCheckFile;
    }
}
