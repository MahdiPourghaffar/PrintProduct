using MediatR;
using PrintProduct.Application.Contracts.Persistence;
using PrintProduct.Application.Features.Product.Dtos;

namespace PrintProduct.Application.Features.Product.Queries.GetProductsList;

public class GetProductsListQueryHandler(IProductRepository repository) : IRequestHandler<GetProductsListQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _repository = repository;

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        return products.Select(p => new ProductDto
        {
        });
    }
}