using MediatR;
using PrintProduct.Application.Contracts.Persistence;

namespace PrintProduct.Application.Features.Product.Commands.UpdateProduct;
public class UpdateProductCommandHandler(IProductRepository repository) : IRequestHandler<UpdateProductCommand, int>
{
    public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existingProduct == null)
            throw new Exception("Product not found");

        existingProduct.Update(
            request.Product.ProductGroupId,
            request.Product.WorkTypeId,
            request.Product.ProductType,
            request.Product.PrintSide,
            request.Product.SheetDimensionId,
            request.Product.FileExtension,
            request.Product.IsCmyk,
            request.Product.IsCalculatePrice
        );

        await repository.UpdateAsync(existingProduct, cancellationToken);

        return existingProduct.Id;
    }
}