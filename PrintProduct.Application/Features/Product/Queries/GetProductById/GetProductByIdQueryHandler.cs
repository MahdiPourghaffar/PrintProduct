using MediatR;
using PrintProduct.Application.Contracts.Persistence;
using PrintProduct.Application.Features.Product.Dtos;

namespace PrintProduct.Application.Features.Product.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _repository = repository;

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null) return null;

        return new ProductDto
        {
        };
    }
}